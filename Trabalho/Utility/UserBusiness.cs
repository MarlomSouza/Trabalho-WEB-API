using System;
using System.Globalization;
using System.Linq;
using Trabalho.DataContext;
using Trabalho.Models;

namespace Trabalho.Utility
{
    public class UserBusiness
    {
        private APIContext db = new APIContext();


        public void CriarUsuarioFaceBook(string nome, string email, string dataNascimento)
        {
            
            var dataNascimentoSenha = DateTime.ParseExact(dataNascimento, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyyddMM");
            User us = new User()
            {
                Nome = nome,
                Email = email,
                Senha = dataNascimentoSenha,
                Tarefas = null
            };
            db.Users.Add(us);
            db.SaveChanges();
        }

        public bool EmailCadastrado(string email)
        {
            User user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            return user != null;
        }

    }
}