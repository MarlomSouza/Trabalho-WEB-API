using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho.DataContext;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    public class UsersController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Users
        /// <summary>
        /// Método responsável por retornar todos os usuários
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        /// <summary>
        /// Método responsável por retornar um usuário específico
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Método responsável por atualizar ou salvar um novo usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="user">Entidade do usuário</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            user.Id = id;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(user);
        }

        // POST: api/Users
        /// <summary>
        /// Método responsável por salvar um novo usuário
        /// </summary>
        /// <param name="user">Entidade do usuário</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (GetUsers().Any(u => u.Email.Equals(user.Email)))
                return BadRequest("Email já cadastrado!");

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Método responsável por deletar um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Método responsável por verificar se o usuário já existe
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}