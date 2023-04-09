using MyApp.ServiceModel.Types;
using System.Collections.Generic;

namespace MyApp.ServiceModel.Tasks.Query;

public class GetAllTodoResponse
{
    public List<Todo> Results { get; set; }
}
