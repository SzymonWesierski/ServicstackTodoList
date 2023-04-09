using MyApp.ServiceModel.Types;
using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Query;

[Route("/todayTodo", "GET")]
public class GetTodayTodoQuery : IReturn<GetTodayTodoResponse> {}
