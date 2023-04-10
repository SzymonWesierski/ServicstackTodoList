using System;
using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MyApp.ServiceInterface;
using MyApp.ServiceModel.Tasks.Command;
using MyApp.ServiceModel.Tasks.Query;
using MyApp.ServiceModel.Types;
using Xunit;

namespace MyApp.Tests.IntegrationTest;

public class AppHost : AppSelfHostBase
{
    public AppHost() : base("IntegrationTest", typeof(TodoService).Assembly) { }

    public override void Configure(Container container)
    {
        container.Register<IDbConnectionFactory>(c =>
            new OrmLiteConnectionFactory("Server=localhost;User Id=postgres;Password=admin;Database=Test;Pooling=true;MinPoolSize=0;MaxPoolSize=200", PostgreSqlDialect.Provider));

        using var db = container.Resolve<IDbConnectionFactory>().Open();
        db.CreateTableIfNotExists<Todo>();
        db.DeleteAll<Todo>();
    }

}

public class IntegrationTest : IDisposable
{
    const string BaseUri = "http://localhost:2000/";
    private ServiceStackHost appHost;

    public IntegrationTest()
    {
        appHost = new AppHost().Init().Start(BaseUri);
         
    }

    public void Dispose()
    {
        appHost.Dispose();
    }

    [Fact]
    public void Can_GET_and_Create_Todo()
    {
        var todos = new JsonServiceClient(BaseUri);

        //GET /todo
        var all = todos.Get(new GetAllTodoQuery());
        Assert.Equal(0, all.Results.Count);

        //POST /todo
        var todo = todos.Post(new CreateTodoCommand
        {
            Title = "IntegrationTest",
            Progress = 20,
            Description = "test",
            DateAndTimeOfExpiry = new DateTime(2023, 3, 9, 16, 5, 0, 0),
        });
        Assert.Equal(1, todo.Id);

        //GET /todo
        all = todos.Get(new GetAllTodoQuery());
        Assert.Equal(1, all.Results.Count);
    }
}
