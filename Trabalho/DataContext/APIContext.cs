using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Trabalho.Models;

namespace Trabalho.DataContext
{
    public class APIContext: DbContext
    {
        public APIContext() : base("TrabalhoContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        public DbSet<ListaTarefa> ListaTarefas { get; set; }

        //Teste
    }
}