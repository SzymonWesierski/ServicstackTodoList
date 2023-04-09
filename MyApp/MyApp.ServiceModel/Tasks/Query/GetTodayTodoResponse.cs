using MyApp.ServiceModel.Types;
using System.Collections.Generic;

namespace MyApp.ServiceModel.Tasks.Query;

public class GetTodayTodoResponse {
    public List<Todo> todos { get; set; }
}
