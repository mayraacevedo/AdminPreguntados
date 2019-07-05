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
using ProyectoG.ViewModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ProyectoG.Controllers
{
    public class SalonesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Salones
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
           
            return View(db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value)));
        }

        // GET: Salones/Details/5
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

            var SalonJView = new JugadoresSalonesView();
            SalonJView.Salones = new tbl_Salones();
            

            SalonJView.Salones = db.tbl_Salones.Find(id);
            var Jugadores= db.tbl_JugadoresSalones.SqlQuery("sp_ConsultaJugadoresxSalon @IdSalon", new SqlParameter("@IdSalon", id)).ToList();

            SalonJView.CantJugadores = Jugadores.Select(x => x.IdJugador).Count();

            return View(SalonJView);
        }

        // GET: Salones/Create
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

            return View();
        }

        // POST: Salones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( tbl_Salones tbl_Salones)
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
            
            
            var IdAcceso = Aletario();
            
            tbl_Salones.IdAdministrador = Convert.ToInt32(Request.Cookies["RolCookie"].Value);
            var ResultadoExist = db.tbl_Salones.SqlQuery("sp_ExistIDAccesoSalon @IdAcceso", new SqlParameter("@IdAcceso", IdAcceso)).ToList();

            while (ResultadoExist.Count > 0)
            {
                IdAcceso = Aletario();
                ResultadoExist = db.tbl_Salones.SqlQuery("sp_ExistIDAccesoSalon @IdAcceso", new SqlParameter("@IdAcceso", IdAcceso)).ToList();
            }
           

            var Reultado = db.tbl_Salones.SqlQuery("sp_CrearSalon @IdAdmin,@idAcceso", new SqlParameter("@IdAdmin", tbl_Salones.IdAdministrador), new SqlParameter("@idAcceso", IdAcceso)).ToList();
            ViewBag.Error = "Salón creado con éxito, el Id de acceso es el: "+IdAcceso;
            return View();
            
        }

        private string Aletario()
        {
            Random obj = new Random();

            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            int longitud = posibles.Length;

            char letra;

            int longitudnuevacadena = 4;

            string nuevacadena = "";

            for (int i = 0; i < longitudnuevacadena; i++)

            {

                letra = posibles[obj.Next(longitud)];

                nuevacadena += letra.ToString();

            }

            return nuevacadena;
        }
        // GET: Salones/Edit/5
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
            tbl_Salones tbl_Salones = db.tbl_Salones.Find(id);
            if (tbl_Salones == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Salones);
        }

        // POST: Salones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_Salones tbl_Salones)
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

            var IDAc = Request["IdAcceso"];
            if (IDAc == "")
            {

                return View(tbl_Salones);
            }
                tbl_Salones.IdAdministrador =Convert.ToInt32(Request.Cookies["RolCookie"].Value);
                var Reultado = db.tbl_Salones.SqlQuery("sp_ModificarSalon @idAcceso,@id", new SqlParameter("@idAcceso", tbl_Salones.IdAcceso), new SqlParameter("@id", tbl_Salones.Id)).ToList();
            
                return RedirectToAction("Index");
            
         
        }

        // GET: Salones/Delete/5
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
            tbl_Salones tbl_Salones = db.tbl_Salones.Find(id);
            if (tbl_Salones == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Salones);
        }

        // POST: Salones/Delete/5
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

            tbl_Salones tbl_Salones = db.tbl_Salones.Find(id);
            db.tbl_Salones.Remove(tbl_Salones);
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
