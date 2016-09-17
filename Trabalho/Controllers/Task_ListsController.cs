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
    public class Task_ListsController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Task_Lists
        public IQueryable<Tarefa> GetTarefas()
        {
            return db.Tarefas;
        }

        // GET: api/Task_Lists/5
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult GetTarefa(int id)
        {
            //List<Tarefa> tarefas = db.Tarefas.Where(t => t.UserId.Equals(id)).ToList();

            var tarefas = db.Users.Where(u => u.Id.Equals(id)).FirstOrDefault().Tarefas;

            if (tarefas == null)
            {
                return NotFound();
            }

            return Ok(tarefas);

            //return db.Tarefas.Where(t => t.UserId.Equals(id));
        }

        // PUT: api/Task_Lists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarefa(int id, Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            db.Entry(tarefa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Task_Lists
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult PostTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarefas.Add(tarefa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarefa.Id }, tarefa);
        }

        // DELETE: api/Task_Lists/5
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult DeleteTarefa(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            db.Tarefas.Remove(tarefa);
            db.SaveChanges();

            return Ok(tarefa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarefaExists(int id)
        {
            return db.Tarefas.Count(e => e.Id == id) > 0;
        }
    }
}