using MyApp.ServiceModel.Types;
using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Query;

[Route("/todo", "GET")]
public class GetTodoQuery : IReturn<Todo>
{
    public int Id { get; set; }
}