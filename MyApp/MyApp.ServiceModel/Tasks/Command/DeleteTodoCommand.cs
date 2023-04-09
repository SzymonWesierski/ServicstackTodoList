using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Command;

[Route("/todo", "DELETE")]
public class DeleteTodoCommand
{
    public int Id { get; set; }
}
