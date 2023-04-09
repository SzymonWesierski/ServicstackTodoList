using ServiceStack;
using MyApp.ServiceModel.Tasks.Query;
using ServiceStack.OrmLite;
using MyApp.ServiceModel.Types;
using MyApp.ServiceModel.Tasks.Command;
using System;

namespace MyApp.ServiceInterface;

public class TodoService : Service
{
    public GetTodoResponse Get(GetTodoQuery query)
    {
        // Get todo by Id
        return new GetTodoResponse { 
            todo = Db.SingleById<Todo>(query.Id) 
        };
    }

    public GetAllTodoResponse Get(GetAllTodoQuery query)
    {
        //Get all todos
        return new GetAllTodoResponse { 
            Results = Db.Select<Todo>()
        };
    }

    public GetTodayTodoResponse Get(GetTodayTodoQuery query)
    {
        //Get today todos
        return new GetTodayTodoResponse{
            todos = Db.Select<Todo>(x => x.DateAndTimeOfExpiry == DateTime.Today)
        };
    }

    //Commented due to servicestack free licence
    //The free-quota limit on '10 ServiceStack Operations' has been reached
    //public GetTomorrowTodoResponse Get(GetTomorrowTodoQuery query)
    //{
    //    //Get tomorrow todos
    //    return new GetTomorrowTodoResponse
    //    {
    //        todos = Db.Select<Todo>(x => x.DateAndTimeOfExpiry == DateTime.Today.AddDays(1))
    //    };
    //}

    public GetWeekTodoResponse Get(GetWeekTodoQuery query)
    {
        //Get current week todos
        return new GetWeekTodoResponse
        {
            todos = Db.Select<Todo>(x => x.DateAndTimeOfExpiry >= DateTime.Today && x.DateAndTimeOfExpiry <= DateTime.Today.AddDays(7))
        };
    }

    public CreateTodoResponse Post(CreateTodoCommand command)
    {
        //Create todo

        var todo = new Todo
        {
            Title = command.Title,
            Progress = command.Progress,
            Description = command.Description,
            DateAndTimeOfExpiry = command.DateAndTimeOfExpiry,
        };

        var todoId = Db.Insert(todo);

        return new CreateTodoResponse
        {
            Id = (int)todoId,
        };
    }

    public UpdateTodoResponse Put(UpdateTodoCommand command)
    {
        //Update todo

        var todoId = Db.Update(new Todo { 
            Id = command.Id,
            Title = command.Title,
            Progress = command.Progress >= 100 ? 100 : command.Progress,
            Description = command.Description,
            DateAndTimeOfExpiry = command.DateAndTimeOfExpiry,
            IsCompleted = command.Progress >= 100 ? true : false,
        });

        return new UpdateTodoResponse
        {
            Id = todoId,
        };
    }

    public UpdateProgressTodoResponse Put(UpdateProgressTodoCommand command)
    {
        //Update Progress todo

        var todo = Db.SingleById<Todo>(command.Id);

        Db.Update(new Todo
        {
            Id = command.Id,
            Progress = command.Progress >= 100 ? 100 : command.Progress,
            Title = todo.Title,
            Description = todo.Description,
            DateAndTimeOfExpiry = todo.DateAndTimeOfExpiry,
            IsCompleted = command.Progress >= 100 ? true : false,
        });

        return new UpdateProgressTodoResponse
        {
            Id = command.Id,
        };
    }

    public DeleteTodoResponse delete(DeleteTodoCommand command)
    {
        //delete todo

        Db.DeleteById<Todo>(command.Id);

        return new DeleteTodoResponse
        {
            Id = command.Id,
        };
    }
}