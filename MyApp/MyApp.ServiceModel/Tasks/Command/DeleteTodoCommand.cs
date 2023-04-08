using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ServiceModel.Tasks.Command
{
    [Route("/todo", "DELETE")]
    public class DeleteTodoCommand
    {
        public int Id { get; set; }
    }
}
