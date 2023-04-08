using ServiceStack.DataAnnotations;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ServiceModel.Tasks.Command
{
    [Route("/todo/{Id}", "PUT")]
    public class UpdateProgressTodoCommand : IReturn<UpdateProgressTodoResponse>
    {
        public int Id { get; set; }
        public byte Progress { get; set; }
    }
}
