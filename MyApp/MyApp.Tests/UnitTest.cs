using Xunit;
using ServiceStack;
using ServiceStack.Testing;
using MyApp.ServiceInterface;
using MyApp.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using MyApp.ServiceModel.Tasks.Query;

namespace MyApp.Tests.UnitTest;

public class UnitTest : IDisposable
{
    private ServiceStackHost appHost;

    public UnitTest()
    {
        if (appHost == null)
        {
            appHost = new BasicAppHost().Init();
            var container = appHost.Container;

            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory("Server=localhost;User Id=postgres;Password=admin;Database=Test;Pooling=true;MinPoolSize=0;MaxPoolSize=200", PostgreSqlDialect.Provider));

            container.RegisterAutoWired<TodoService>();

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Todo>();
            }
        }
    }

    public void Dispose()
    {
        appHost.Dispose();
    }

    [Fact]
    public void GetTodo_ReturnsTodo()
    {
        var service = appHost.Container.Resolve<TodoService>();
        //Arrange
        var todo = new Todo { Title = "Todo 1" };
        service.Db.Save(todo);
        var query = new GetTodoQuery { Id = todo.Id };

        //Act
        var response = service.Get(query);

        //Assert
        Assert.Equal(todo.Id, response.todo.Id);
        Assert.Equal(todo.Title, response.todo.Title);
    }

    [Fact]
    public void GetAllTodo_ReturnsAllTodos()
    {
        var service = appHost.Container.Resolve<TodoService>();
        //Arrange
        var todo1 = new Todo { Title = "Todo 1" };
        var todo2 = new Todo { Title = "Todo 2" };
        service.Db.SaveAll(new[] { todo1, todo2 });
        var query = new GetAllTodoQuery();

        //Act
        var response = service.Get(query);

        //Assert
        Assert.Equal(2, response.Results.Count);
        Assert.Equal(todo1.Id, response.Results[0].Id);
        Assert.Equal(todo2.Id, response.Results[1].Id);
    }

    [Fact]
    public void GetTodayTodo_ReturnsTodayTodos()
    {
        var service = appHost.Container.Resolve<TodoService>();
        //Arrange
        var todo1 = new Todo { Title = "Todo 1", DateAndTimeOfExpiry = DateTime.Today };
        var todo2 = new Todo { Title = "Todo 2", DateAndTimeOfExpiry = DateTime.Today.AddDays(1) };
        service.Db.SaveAll(new[] { todo1, todo2 });
        var query = new GetTodayTodoQuery();

        //Act
        var response = service.Get(query);

        //Assert
        Assert.Equal(1, response.todos.Count);
        Assert.Equal(todo1.Id, response.todos[0].Id);
    }

    [Fact]
    public void GetTomorrowTodo_ReturnsTomorrowTodos()
    {
        var service = appHost.Container.Resolve<TodoService>();
        //Arrange
        var todo1 = new Todo { Title = "Todo 1", DateAndTimeOfExpiry = DateTime.Today.AddDays(1) };
        var todo2 = new Todo { Title = "Todo 2", DateAndTimeOfExpiry = DateTime.Today.AddDays(2) };
        service.Db.SaveAll(new[] { todo1, todo2 });
        var query = new GetTomorrowTodoQuery();

        //Act
        var response = service.Get(query);

        //Assert
        Assert.Equal(1, response.todos.Count);
        Assert.Equal(todo1.Id, response.todos[0].Id);
    }
}
