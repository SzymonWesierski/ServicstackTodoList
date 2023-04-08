using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace MyApp.ServiceModel.Types;

public class Todo
{
    [AutoIncrement]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int Progress { get; set; }
    [Input(Type = "textarea")]
    public string Description { get; set; }
    public DateTime DateAndTimeOfExpiry { get; set; }
    public Boolean IsCompleted { get; set; }
}