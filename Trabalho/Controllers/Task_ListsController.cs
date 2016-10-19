using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho.DataContext;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    public class Task_ListsController : ApiController
    {
        private APIContext db = new APIContext();

        /// <summary>
        /// Método responsável por retornar as listas de tarefas de um usuário.
        /// </summary>
        /// <param name="_id">Identificador do usuário</param>
        /// <returns></returns>
        public IQueryable<ListaTarefa> GetTodasListasdeTarefaUsuario(int _id)
        {
            return db.ListaTarefas.Where(t => t.UserId.Equals(_id));
        }

        /// <summary>
        /// Retorna todas as tarefas da lista
        /// </summary>
        /// <param name="id">identificador da lista</param>
        /// <param name="_id">identificar do usuario</param>
        /// <returns></returns>
        public IQueryable<ListaTarefa> GetTarefaEspecifica(int id, int _id)
        {
            return db.ListaTarefas.Where(t => t.UserId.Equals(_id) && t.Id.Equals(id));
        }

        // PUT: api/Task_list/5
        /// <summary>
        /// Atualização da lista de tarefas: nome e cor
        /// </summary>
        /// <param name="id">Identificador da lista</param>
        /// <param name="listaTarefa">Entidade ListaTarefa</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutListaTarefa(int id, ListaTarefa listaTarefa)
        {
            using (var context = new APIContext())
            {
                int userId = context.ListaTarefas.Find(id).UserId;
                listaTarefa.UserId = userId;
            }
            listaTarefa.Id = id;

            db.Entry(listaTarefa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaTarefaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(listaTarefa);
        }

        // POST: api/Task_list
        /// <summary>
        /// Cadastro de lista de tarefas
        /// </summary>
        /// <param name="listaTarefa">Método responsável por salvar uma lista de tarefas</param>
        /// <param name="_id">Id do usuario</param>
        /// <returns></returns>
        [ResponseType(typeof(ListaTarefa))]
        public IHttpActionResult PostListaTarefa(ListaTarefa listaTarefa, int _id)
        {
            User tempUser = db.Users.Find(_id);

            if (tempUser == null)
                return NotFound();
                //return BadRequest("Usuario não existe!");

            listaTarefa.UserId = _id;

            db.ListaTarefas.Add(listaTarefa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = listaTarefa.Id }, listaTarefa);
        }

        /// <summary>
        /// Método responsável por deletar uma lista de tarefas
        /// </summary>
        /// <param name="id">Identificador da lista de tarefas</param>
        /// <returns></returns>
        // DELETE: api/Task_list/5
        [ResponseType(typeof(ListaTarefa))]
        public IHttpActionResult DeleteListaTarefa(int id)
        {
            ListaTarefa listaTarefa = db.ListaTarefas.Find(id);
            if (listaTarefa == null)
                return NotFound();

            db.ListaTarefas.Remove(listaTarefa);
            db.SaveChanges();

            return Ok();
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
        /// Método responsável por verificar se uma lista de tarefas já existe
        /// </summary>
        /// <param name="id">Identificador da lista de tarefas</param>
        /// <returns></returns>
        private bool ListaTarefaExists(int id)
        {
            return db.ListaTarefas.Count(e => e.Id == id) > 0;
        }
    }
}