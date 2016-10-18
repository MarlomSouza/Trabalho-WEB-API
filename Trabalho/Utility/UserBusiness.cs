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
        private EnviaEmail enviaEmail = new EnviaEmail();

        public void CriarUsuarioFaceBook(string nome, string email)
        {
            User us = new User()
            {
                Nome = nome,
                Email = email
            };
            enviaEmail.CriaEmail(us,"Senha do sistema de tarefa");
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