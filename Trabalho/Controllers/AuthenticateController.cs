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

namespace Trabalho.Controllers
{
    public class AuthenticateController : ApiController
    {
        private APIContext db = new APIContext();
        private EnviaEmail enviaEmail = new EnviaEmail();
        private GetFacebookLoginUrl getFacebookLoginUrl = new GetFacebookLoginUrl();

        // GET: api/Authenticate
        public IEnumerable<string> Get()
        {
            getFacebookLoginUrl.GetFacebookLogin("");
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authenticate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: /Authenticate
        [ActionName("Authenticate")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostAuthenticate([FromBody]User user)
        {
            User usuario = new User();
            if (!string.IsNullOrEmpty(user.facebook_token))
                getFacebookLoginUrl.GetFacebookLogin(user.facebook_token);
            else
            {
                if(string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                    return BadRequest();

                usuario = db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
                if (!user.Senha.Equals(usuario.Senha))
                    return BadRequest();
            }
            return Ok(usuario);
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
