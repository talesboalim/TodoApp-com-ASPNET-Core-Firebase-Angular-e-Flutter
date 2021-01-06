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
    }
}