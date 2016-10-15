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

namespace Trabalho.Controllers
{
    public class AuthenticateController : ApiController
    {
        private APIContext db = new APIContext();
        private EnviaEmail enviaEmail = new EnviaEmail();
        private FacebookConnection FacebookConnection = new FacebookConnection();
        private UserBusiness _userBusiness = new UserBusiness();

        // POST: /Authenticate
        [ActionName("Authenticate")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostAuthenticate([FromBody]User user)
        {
            User usuario;
            string parametros;
            if (!string.IsNullOrEmpty(user.facebook_token))
            {
                parametros = FacebookConnection.GetFacebookParameters(user.facebook_token);
                var jo = JObject.Parse(parametros);
                if (!_userBusiness.EmailCadastrado(jo["email"].ToString()))
                {
                    _userBusiness.CriarUsuarioFaceBook(jo["name"].ToString(), jo["email"].ToString(), jo["birthday"].ToString());
                }
                
            }
            else
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                    return BadRequest();

                usuario = db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
                if (!user.Senha.Equals(usuario.Senha))
                    return BadRequest();
            }
            return Ok();
        }

        // POST: Authenticate/forgot_password
        [ActionName("Forgot_password")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostForgot_password([FromBody]User user)
        {
            var existeUsuario = db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            if (existeUsuario != null)
            {
                User usuario = existeUsuario;
                if (enviaEmail.CriaEmail(usuario))
                {
                    db.SaveChanges();
                    return Ok("Senha Alterada");
                }
            }
            return BadRequest("Email não existe na base!");
        }

        // PUT: api/Authenticate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authenticate/5
        public void Delete(int id)
        {
        }
    }
}
