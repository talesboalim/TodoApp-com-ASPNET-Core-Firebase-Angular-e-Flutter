using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.Queries
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;
        public TodoQueryTests()
        {
            TodoItem todo;
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", "usuario A", DateTime.Now));
            todo = new TodoItem("Tarefa 2", "usuario A", DateTime.Now);
            todo.MarkAsDone();
            _items.Add(todo);
            todo = new TodoItem("Tarefa 3", "Tales Boalim", DateTime.Now);
            todo.MarkAsDone();
            _items.Add(todo);
            _items.Add(new TodoItem("Tarefa 4", "usuario A", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 5", "Tales Boalim", DateTime.Now));
            todo = new TodoItem("Tarefa 6", "Tales Boalim", DateTime.Now.AddDays(-1));
            todo.MarkAsDone();
            _items.Add(todo);

        }

        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_Tales_Boalim()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("Tales Boalim"));
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_marcadas_como_concluidas_do_usuario_Tales_Boalim()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("Tales Boalim"));
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_em_aberto_do_usuario_Usuario_A()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("usuario A"));
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_concluidas_de_hoje_do_usuario_Tales_Boalim()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetByPeriod("Tales Boalim", DateTime.Now, true));
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_em_aberto_de_hoje_do_usuario_Tales_Boalim()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetByPeriod("Tales Boalim", DateTime.Now, false));
            Assert.AreEqual(1, result.Count());
        }

    }
}