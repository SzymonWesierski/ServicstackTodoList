using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace MyApp.ServiceModel.Tasks.Command;

[Route("/todo/{Id}", "PUT")]
public class UpdateTodoCommand : IReturn<UpdateTodoResponse>
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public byte Progress { get; set; }
    [Input(Type = "textarea")]
    public string Description { get; set; }
    public DateTime DateAndTimeOfExpiry { get; set; }
}
