using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Trabalho.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public List<Tarefa> Tarefas { get; set; }
    }
}