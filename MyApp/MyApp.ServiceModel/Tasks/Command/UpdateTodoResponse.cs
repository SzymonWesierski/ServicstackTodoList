using ServiceStack.DataAnnotations;
using ServiceStack;
using System;

namespace MyApp.ServiceModel.Tasks.Command
{
    public class UpdateTodoResponse
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public byte Progress { get; set; }
        [Input(Type = "textarea")]
        public string Description { get; set; }
        public DateTime DateAndTimeOfExpiry { get; set; }
    }
}
