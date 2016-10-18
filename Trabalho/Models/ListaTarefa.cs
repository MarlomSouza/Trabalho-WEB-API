using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trabalho.Models
{
    public class ListaTarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}