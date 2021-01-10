using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Domain.Entities
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _validTodo = new TodoItem("Lavar a cozinha", "Tales Boalim", DateTime.Now);

        [TestMethod]
        public void Dado_um_novo_todo_o_mesmo_nao_pode_estar_como_concluido()
        {
            Assert.AreEqual(_validTodo.Done, false);
        }
    }
}