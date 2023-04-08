using MyApp.ServiceModel.Types;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ServiceModel.Tasks.Query
{
    [Route("/weekTodo", "GET")]
    public class GetWeekTodoQuery : IReturn<Todo>{}
}
