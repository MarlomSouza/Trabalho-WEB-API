using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trabalho.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public DateTime Data { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}