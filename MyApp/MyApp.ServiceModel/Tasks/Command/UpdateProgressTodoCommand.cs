using ServiceStack;

namespace MyApp.ServiceModel.Tasks.Command;

[Route("/todo/{Id}", "PUT")]
public class UpdateProgressTodoCommand : IReturn<UpdateProgressTodoResponse>
{
    public int Id { get; set; }
    public byte Progress { get; set; }
}

