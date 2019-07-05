using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoG.Context;
using ProyectoG.Models;

namespace ProyectoG.Controllers
{
    public class RespuestasController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Respuestas
        public ActionResult Index()
        {
            var tbl_Respuestas = db.tbl_Respuestas.Include(t => t.tbl_Preguntas);
            return View(tbl_Respuestas.ToList());
        }

        // GET: Respuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            if (tbl_Respuestas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Respuestas);
        }

        // GET: Respuestas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pregunta = new SelectList(db.tbl_Preguntas, "Id", "Pregunta");
            return View();
        }

        // POST: Respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Pregunta,Respuesta,Respuesta_Correcta")] tbl_Respuestas tbl_Respuestas)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Respuestas.Add(tbl_Respuestas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pregunta = new SelectList(db.tbl_Preguntas, "Id", "Pregunta", tbl_Respuestas.Id_Pregunta);
            return View(tbl_Respuestas);
        }

        // GET: Respuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            if (tbl_Respuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pregunta = new SelectList(db.tbl_Preguntas, "Id", "Pregunta", tbl_Respuestas.Id_Pregunta);
            return View(tbl_Respuestas);
        }

        // POST: Respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Pregunta,Respuesta,Respuesta_Correcta")] tbl_Respuestas tbl_Respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Respuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pregunta = new SelectList(db.tbl_Preguntas, "Id", "Pregunta", tbl_Respuestas.Id_Pregunta);
            return View(tbl_Respuestas);
        }

        // GET: Respuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            if (tbl_Respuestas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Respuestas);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            db.tbl_Respuestas.Remove(tbl_Respuestas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
