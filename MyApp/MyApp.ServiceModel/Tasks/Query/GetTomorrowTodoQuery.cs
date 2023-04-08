using MyApp.ServiceModel.Types;
using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Query
{
    [Route("/tomorrowTodo", "GET")]
    public class GetTomorrowTodoQuery : IReturn<Todo> {}
}
