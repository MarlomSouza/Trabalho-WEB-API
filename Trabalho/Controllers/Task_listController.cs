﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho.DataContext;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    public class Task_listController : ApiController
    {
        private APIContext db = new APIContext();

        public IQueryable<Tarefa> GetTodasTarefaUsuario(int _id)
        {
            return db.ListaTarefas.Where(t => t.UserId.Equals(_id)).SelectMany(tarefa => tarefa.Tarefas);
        }

        public IQueryable<Tarefa> GetTodasTarefadaLista(int _listaId, int user_id)
        {
            return db.ListaTarefas.Where(t => t.UserId.Equals(user_id) && t.Id.Equals(_listaId)).SelectMany(tarefa => tarefa.Tarefas);
        }

        // PUT: api/Task_Lists/5
        /// <summary>
        /// Atualização do status da tarefa: status (0: a fazer, 1: feito).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarefa(int _listaId, int task_id, Tarefa tarefa)
        {
            tarefa.Id = task_id;
            tarefa.Data = DateTime.Now;
            tarefa.ListaTarefaId = _listaId;

            db.Entry(tarefa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(task_id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(tarefa);
        }

        // POST: api/Task_Lists
        /// <summary>
        /// Cadastrar uma nova tarefa e relacionar essa tarefa com a lista de tarefas.
        /// </summary>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult PostTarefa(int _id, Tarefa tarefa)
        {
            if (db.ListaTarefas.Find(_id) == null)
                return BadRequest("Lista de tarefa não existe!");

            tarefa.ListaTarefaId = _id;
            tarefa.Data = DateTime.Now;

            db.Tarefas.Add(tarefa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarefa.Id }, tarefa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }

        private bool TarefaExists(int id)
        {
            return db.Tarefas.Count(e => e.Id == id) > 0;
        }
    }
}