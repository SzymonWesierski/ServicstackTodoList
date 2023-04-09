using MyApp.ServiceModel.Types;
using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Query;

[Route("/weekTodo", "GET")]
public class GetWeekTodoQuery : IReturn<GetWeekTodoResponse>{}
