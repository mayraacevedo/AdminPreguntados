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
    public class AdministradoresController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Administradores
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

            var tbl_Administradores = db.tbl_Administradores.Include(t => t.tbl_Instituciones);
            return View(tbl_Administradores.ToList());
        }

        // GET: Administradores/Details/5
        public ActionResult Details(int? id)
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Administradores tbl_Administradores = db.tbl_Administradores.Find(id);
            if (tbl_Administradores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Administradores);
        }

        // GET: Administradores/Create
        public ActionResult Create()
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

            ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre");
            return View();
        }

        // POST: Administradores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Administradores tbl_Administradores)
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
            
                var Correo = Request["Correo"].ToString();
                if (!Correo.Contains(".edu"))
                {
                    ViewBag.Error = "El correo debe ser un .edu";
                    ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre");
                    return View();

                }
                else
                {
                    tbl_Administradores.Rol = "Docente";
                tbl_Administradores.Estado = true;
                    
                    db.tbl_Administradores.Add(tbl_Administradores);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
        }

        // GET: Administradores/Edit/5
        public ActionResult Edit(int? id)
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Administradores tbl_Administradores = db.tbl_Administradores.Find(id);
            if (tbl_Administradores == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
            return View(tbl_Administradores);
        }

        // POST: Administradores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,FechaNacimiento,IdInstitucion,Correo,Clave,Rol")] tbl_Administradores tbl_Administradores)
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

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Administradores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
            return View(tbl_Administradores);
        }

        // GET: Administradores/Delete/5
        public ActionResult Delete(int? id)
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Administradores tbl_Administradores = db.tbl_Administradores.Find(id);
            if (tbl_Administradores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Administradores);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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

            tbl_Administradores tbl_Administradores = db.tbl_Administradores.Find(id);
            db.tbl_Administradores.Remove(tbl_Administradores);
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
