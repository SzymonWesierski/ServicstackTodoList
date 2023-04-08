using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using MyApp.ServiceInterface;
using MyApp.ServiceModel;

namespace MyApp.Tests;

public class UnitTest
{
    private readonly ServiceStackHost appHost;

    public UnitTest()
    {
        appHost = new BasicAppHost().Init();
        appHost.Container.AddTransient<TodoService>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => appHost.Dispose();

    [Test]
    public void Can_call_MyServices()
    {
        var service = appHost.Container.Resolve<TodoService>();

        //var response = (HelloResponse)service.Any(new CreateTaskCommand { Name = "World" });

        //Assert.That(response.Result, Is.EqualTo("Hello, World!"));
    }
}