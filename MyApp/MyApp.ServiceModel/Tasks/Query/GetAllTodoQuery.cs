using MyApp.ServiceModel.Types;
using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Query;

[Route("/todos", "GET")]
public class GetAllTodoQuery : IReturn<GetAllTodoResponse> { }
