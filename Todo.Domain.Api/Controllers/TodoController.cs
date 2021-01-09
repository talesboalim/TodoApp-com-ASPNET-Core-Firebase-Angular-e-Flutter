using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAll(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetAll("Tales Boalim");
        }

        [Route("done")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDone(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetAllDone("Tales Boalim");
        }

        [Route("undone")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndone(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetAllUndone("Tales Boalim");
        }

        [Route("done/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForToday(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetByPeriod(
                "Tales Boalim",
                DateTime.Now.Date,
                true
            );
        }

        [Route("undone/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForToday(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetByPeriod(
                "Tales Boalim",
                DateTime.Now.Date,
                false
            );
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForTomorrow(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetByPeriod(
                "Tales Boalim",
                DateTime.Now.Date.AddDays(1),
                true
            );
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForTomorrow(
            [FromServices] ITodoRepository repository

        )
        {
            return repository.GetByPeriod(
                "Tales Boalim",
                DateTime.Now.Date.AddDays(1),
                false
            );
        }



        [Route("")]//sem rota específica para criar o create
        [HttpPost]
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command, //Quando fizerem um post o Frombory converte o JSON no objeto
            [FromServices] TodoHandler handler//A partir do que foi configurado no Startup.cs no AddTransient do Handler, vai resolver a dependência rapidamente
        )
        {
            command.User = "Tales Boalim";
            //O command recebe os dados e passa para o Handler pois é ele quem vai tratar
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("")]
        [HttpPut]
        public GenericCommandResult Update(
            [FromBody] UpdateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = "Tales Boalim";
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public GenericCommandResult MarkAsDone(
            [FromBody] MarkTodoAsDoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = "Tales Boalim";
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        public GenericCommandResult MarkAsUndone(
            [FromBody] MarkTodoAsUndoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = "Tales Boalim";
            return (GenericCommandResult)handler.Handle(command);
        }


    }
}