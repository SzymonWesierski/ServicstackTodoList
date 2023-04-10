using Funq;
using ServiceStack;
using NUnit.Framework;
using MyApp.ServiceInterface;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using MyApp.ServiceModel.Types;
using MyApp.ServiceModel.Tasks.Query;
using MyApp.ServiceModel.Tasks.Command;
using System;


namespace MyApp.Tests;

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

public class IntegrationTest
{
    const string BaseUri = "http://localhost:2000/";
    private ServiceStackHost appHost;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        //Start your AppHost on OneTimeSetUp
        appHost = new AppHost()
            .Init()
            .Start(BaseUri);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => appHost.Dispose();


    [Test]
    public void Can_GET_and_Create_Todo()
    {
        var todos = new JsonServiceClient(BaseUri);

        //GET /todo
        var all = todos.Get(new GetAllTodoQuery());
        Assert.That(all.Results.Count, Is.EqualTo(0));

        //POST /todo
        var todo = todos.Post(new CreateTodoCommand
        {
            Title = "IntegrationTest",
            Progress = 20,
            Description = "test",
            DateAndTimeOfExpiry = new DateTime(2023, 3, 9, 16, 5, 0, 0),
        });
        Assert.That(todo.Id, Is.EqualTo(1));

        //GET /todo
        all = todos.Get(new GetAllTodoQuery());
        Assert.That(all.Results.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Can_GET_and_Create_Todo2()
    {
        var todos = new JsonServiceClient(BaseUri);

        //GET /todo
        var all = todos.Get(new GetAllTodoQuery());
        Assert.That(all.Results.Count, Is.EqualTo(1));

        //POST /todo
        var todo = todos.Post(new CreateTodoCommand
        {
            Title = "IntegrationTest",
            Progress = 20,
            Description = "test",
            DateAndTimeOfExpiry = new DateTime(2023, 3, 9, 16, 5, 0, 0),
        });
        Assert.That(todo.Id, Is.EqualTo(1));

        //GET /todo
        all = todos.Get(new GetAllTodoQuery());
        Assert.That(all.Results.Count, Is.EqualTo(2));
    }

}