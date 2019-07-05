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
    public class JugadoresController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Jugadores
        public ActionResult Index()
        {
            if (Request.Cookies["RolCookie"] == null)
                return RedirectToAction("Login", "Registro");
            else
            {
                var idC = Request.Cookies["RolCookie"].Value;
                var c = new HttpCookie("RolCookie");
                c.Value = idC;
                c.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Add(c);
            }

            return View(db.tbl_Jugadores.ToList());
        }

        // GET: Jugadores/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Jugadores tbl_Jugadores = db.tbl_Jugadores.Find(id);
            if (tbl_Jugadores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Jugadores);
        }

        // GET: Jugadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Apellidos,Usuario,Password,Genero,Correo,FechaNacimiento")] tbl_Jugadores tbl_Jugadores)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Jugadores.Add(tbl_Jugadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Jugadores);
        }

        // GET: Jugadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Jugadores tbl_Jugadores = db.tbl_Jugadores.Find(id);
            if (tbl_Jugadores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Jugadores);
        }

        // POST: Jugadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Usuario,Password,Genero,Correo,FechaNacimiento")] tbl_Jugadores tbl_Jugadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Jugadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Jugadores);
        }

        // GET: Jugadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Jugadores tbl_Jugadores = db.tbl_Jugadores.Find(id);
            if (tbl_Jugadores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Jugadores);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Jugadores tbl_Jugadores = db.tbl_Jugadores.Find(id);
            db.tbl_Jugadores.Remove(tbl_Jugadores);
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
