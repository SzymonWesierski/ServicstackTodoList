using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Command;

[Route("/todo{Id}", "DELETE")]
public class DeleteTodoCommand : IReturn<DeleteTodoResponse>
{
    public int Id { get; set; }
}
