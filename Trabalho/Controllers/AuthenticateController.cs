using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho.DataContext;
using Trabalho.Models;
using Trabalho.Utility;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace Trabalho.Controllers
{
    public class AuthenticateController : ApiController
    {
        private APIContext db = new APIContext();
        private EnviaEmail enviaEmail = new EnviaEmail();
        private FacebookConnection FacebookConnection = new FacebookConnection();
        private UserBusiness _userBusiness = new UserBusiness();

        /// <summary>
        /// Método responsável pela autenticação do usuário.
        /// </summary>
        /// <param name="user">Usuário a ser autenticado</param>
        /// <returns></returns>
        // POST: /Authenticate
        [ActionName("Authenticate")]
        [ResponseType(typeof(User))]
        [Route("Authenticate/Authenticate")]
        public IHttpActionResult PostAuthenticate([FromBody]User user)
        {
            User usuario;
            string parametros;

            if (!string.IsNullOrEmpty(user.facebook_token))
            {
                parametros = FacebookConnection.GetFacebookParameters(user.facebook_token);
                var jo = JObject.Parse(parametros);
                string email = jo["email"].ToString();
                if (!_userBusiness.EmailCadastrado(email))
                    _userBusiness.CriarUsuarioFaceBook(jo["name"].ToString(), email);

                usuario = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
            else
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                    return BadRequest();

                usuario = db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();

                if(usuario == null)
                    return BadRequest("Email não encontrado!");

                if (!user.Senha.Equals(usuario.Senha))
                    return BadRequest("Senha inválida!");
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Método responsável pela recuperação de senhas
        /// </summary>
        /// <param name="user">Usuário que deseja recuperar a senha</param>
        /// <returns></returns>
        // POST: Authenticate/forgot_password
        [ActionName("Forgot_password")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostForgot_password([FromBody]User user)
        {
            if(string.IsNullOrEmpty(user.Email))
                return BadRequest("Email não preenchido!");

            if (!IsValid(user.Email))
                return BadRequest("Email inválido!");

            var existeUsuario = db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            if (existeUsuario != null)
            {
                User usuario = existeUsuario;
                if (enviaEmail.CriaEmail(usuario, "Renovação de senha"))
                {
                    db.SaveChanges();
                    return Ok("Senha Alterada");
                }
            }
            return BadRequest("Email não existe na base!");
        }

        /// <summary>
        /// Método responsável por validar se o e-mail é válido
        /// </summary>
        /// <param name="emailaddress">E-mail a ser verificado</param>
        /// <returns></returns>
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
