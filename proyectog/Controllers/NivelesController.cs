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
    public class NivelesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Niveles
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

            return View(db.tbl_Niveles.ToList());
        }

        // GET: Niveles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Niveles tbl_Niveles = db.tbl_Niveles.Find(id);
            if (tbl_Niveles == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Niveles);
        }

        // GET: Niveles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Niveles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Nivel,Nivel,MaximoPuntaje,PuntosPregunta")] tbl_Niveles tbl_Niveles)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Niveles.Add(tbl_Niveles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Niveles);
        }

        // GET: Niveles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Niveles tbl_Niveles = db.tbl_Niveles.Find(id);
            if (tbl_Niveles == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Niveles);
        }

        // POST: Niveles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Nivel,Nivel,MaximoPuntaje,PuntosPregunta")] tbl_Niveles tbl_Niveles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Niveles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Niveles);
        }

        // GET: Niveles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Niveles tbl_Niveles = db.tbl_Niveles.Find(id);
            if (tbl_Niveles == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Niveles);
        }

        // POST: Niveles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Niveles tbl_Niveles = db.tbl_Niveles.Find(id);
            db.tbl_Niveles.Remove(tbl_Niveles);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

               
            }
           
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
