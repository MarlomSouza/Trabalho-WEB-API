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

        /// <summary>
        /// Método responsável por salvar as informações do usuário provindas do Facebook
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="email">E-mail do usuário</param>
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

        /// <summary>
        /// Verificar se o email já está cadastrado
        /// </summary>
        /// <param name="email">E-mail a ser verificado</param>
        /// <returns></returns>
        public bool EmailCadastrado(string email)
        {
            User user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            return user != null;
        }

    }
}