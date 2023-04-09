namespace MyApp.ServiceModel.Tasks.Command;

public class UpdateProgressTodoResponse
{
    public int Id { get; set; }
    public string Title { get; set; }  
    public byte Progress { get; set; }
}
