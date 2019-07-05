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
    public class AmigosController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Amigos
        public ActionResult Index()
        {
            return View(db.tbl_Amigos.ToList());
        }

        // GET: Amigos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Amigos tbl_Amigos = db.tbl_Amigos.Find(id);
            if (tbl_Amigos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Amigos);
        }

        // GET: Amigos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdJugadorPrincipal,IdJugadorAmigo")] tbl_Amigos tbl_Amigos)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Amigos.Add(tbl_Amigos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Amigos);
        }

        // GET: Amigos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Amigos tbl_Amigos = db.tbl_Amigos.Find(id);
            if (tbl_Amigos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Amigos);
        }

        // POST: Amigos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdJugadorPrincipal,IdJugadorAmigo")] tbl_Amigos tbl_Amigos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Amigos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Amigos);
        }

        // GET: Amigos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Amigos tbl_Amigos = db.tbl_Amigos.Find(id);
            if (tbl_Amigos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Amigos);
        }

        // POST: Amigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Amigos tbl_Amigos = db.tbl_Amigos.Find(id);
            db.tbl_Amigos.Remove(tbl_Amigos);
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
