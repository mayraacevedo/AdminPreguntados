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
    public class CategoriasController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Categorias
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

            return View(db.tbl_Categorias.ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categorias tbl_Categorias = db.tbl_Categorias.Find(id);
            if (tbl_Categorias == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categorias);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Categoria")] tbl_Categorias tbl_Categorias)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Categorias.Add(tbl_Categorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Categorias);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categorias tbl_Categorias = db.tbl_Categorias.Find(id);
            if (tbl_Categorias == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categorias);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Categoria")] tbl_Categorias tbl_Categorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Categorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Categorias);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categorias tbl_Categorias = db.tbl_Categorias.Find(id);
            if (tbl_Categorias == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Categorias tbl_Categorias = db.tbl_Categorias.Find(id);
            db.tbl_Categorias.Remove(tbl_Categorias);
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
