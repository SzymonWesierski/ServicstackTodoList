using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using MyApp.ServiceInterface;
using MyApp.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using MyApp.ServiceModel.Tasks.Query;

namespace MyApp.Tests;
public class UnitTest
{
    private ServiceStackHost appHost;

    [OneTimeSetUp]
    public void OneTimeSetUp()
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

    [OneTimeTearDown]
    public void OneTimeTearDown() => appHost.Dispose();

    [Test]
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
        Assert.That(response.todo.Id, Is.EqualTo(todo.Id));
        Assert.That(response.todo.Title, Is.EqualTo(todo.Title));
    }

    [Test]
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
        Assert.That(response.Results, Has.Count.EqualTo(2));
        Assert.That(response.Results[0].Id, Is.EqualTo(todo1.Id));
        Assert.That(response.Results[1].Id, Is.EqualTo(todo2.Id));
    }

    [Test]
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
        Assert.That(response.todos, Has.Count.EqualTo(1));
        Assert.That(response.todos[0].Id, Is.EqualTo(todo1.Id));
    }

    [Test]
    public void GetTomorrowTodo_ReturnsTomorrowTodos()
    {
        //var service = appHost.Container.Resolve<TodoService>();
        ////Arrange
        //var todo1 = new Todo { Title = "Todo 1", DateAndTimeOfExpiry = DateTime.Today.AddDays(1) };
        //var todo2 = new Todo { Title = "Todo 2", DateAndTimeOfExpiry = DateTime.Today.AddDays(2) };
        //service.Db.SaveAll(new[] { todo1, todo2 });
        //var query = new GetTomorrowTodoQuery();

        ////Act
        //var response = service.Get(query);

        ////Assert
        //Assert.That(response.todos, Has.Count.EqualTo(1));
        //Assert.That(response.todos[0].Id, Is.EqualTo(todo1.Id));
    }
}