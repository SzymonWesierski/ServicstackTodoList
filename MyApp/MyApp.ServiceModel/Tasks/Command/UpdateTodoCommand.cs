using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ServiceModel.Tasks.Command
{
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
}
