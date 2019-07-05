using ProyectoG.Context;
using ProyectoG.Models;
using ProyectoG.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoG.Controllers
{
    public class JugadoresSalonesController : Controller
    {
        private GameContext db = new GameContext();
        
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

        public ActionResult Agregar()
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

            var JugadorView = new JugadoresSalonesView();
            JugadorView.Jugadores = new List<tbl_Jugadores>();
            var id = Session["IdSalon"];

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id", id);

            Session["JugadorSalonView"] = JugadorView;
            return View(JugadorView);
        }

        [HttpPost, ActionName("Agregar")]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(JugadoresSalonesView JugadorRView)
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

            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

            var IdSalon = Request["IdSalon"];


            if (IdSalon == "")
            {
                ViewBag.Error = "Debe ingresar un id de salon";
                return View(JugadorSalonView);
            }

            var Salones = db.tbl_Salones.SqlQuery("sp_ConsultarSalon @ID", new SqlParameter("@ID", IdSalon)).ToList();

            if (Salones.Count() == 0)
            {
                ViewBag.Error = "El id de salon no existe";
                return View(JugadorSalonView);
            }
            if (JugadorSalonView.Jugadores.Count == 0)
            {
                ViewBag.Error = "Debe ingresar Jugadores";
                return View(JugadorSalonView);
            }

            foreach (var item in JugadorSalonView.Jugadores)
            {
                var Respuesta = db.tbl_JugadoresSalones.SqlQuery("sp_InsertarJugadorSalon @IdJ,@IdS", new SqlParameter("@IdJ", item.Id), new SqlParameter("@IdS", IdSalon)).ToList();
            }

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");
            return View(JugadorRView);

        }

        public ActionResult EditarSalon(int? Id)
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

            var JugadorRView = new JugadoresSalonesView();
            JugadorRView.Jugadores = new List<tbl_Jugadores>();
            JugadorRView.Jugadores = db.tbl_Jugadores.SqlQuery("sp_ConsultaJugadorSalon @IdS", new SqlParameter("@IdS", Id)).ToList();

            Session["IdSalon"] = Id;
            Session["JugadorSalonView"] = JugadorRView;
            return View(JugadorRView);
        }

        [HttpPost, ActionName("EditarSalon")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarSalon(JugadoresSalonesView JugadorRView)
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

            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

            var IdSalon = Session["IdSalon"];


            if (IdSalon == "")
            {
                ViewBag.Error = "Debe ingresar un id de salon";
                return View(JugadorSalonView);
            }

            var Salones = db.tbl_Salones.SqlQuery("sp_ConsultarSalon @ID", new SqlParameter("@ID", IdSalon)).ToList();

            if (Salones.Count() == 0)
            {
                ViewBag.Error = "El id de salon no existe";
                return View(JugadorSalonView);
            }
            if (JugadorSalonView.Jugadores.Count == 0)
            {
                ViewBag.Error = "Debe ingresar Jugadores";
                return View(JugadorSalonView);
            }

            var Lista = db.tbl_JugadoresSalones.SqlQuery("sp_EliminarJugadorSalon @IdS", new SqlParameter("@IdS", IdSalon)).ToList();
            foreach (var item in JugadorSalonView.Jugadores)
            {
                var Respuesta = db.tbl_JugadoresSalones.SqlQuery("sp_InsertarJugadorSalon @IdP,@IdS", new SqlParameter("@IdP", item.Id), new SqlParameter("@IdS", IdSalon)).ToList();

            }


            return RedirectToAction("Index");

        }
        public ActionResult AgregarJugador()
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

            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

            var ListPre = db.tbl_Jugadores.ToList();
            ListPre = ListPre.OrderBy(c => c.Nombres).ToList();

            foreach (var item in JugadorSalonView.Jugadores)
            {
                ListPre = ListPre.Where(x => x.Id != item.Id).ToList();
            }
            ViewBag.Id = new SelectList(ListPre, "Id", "FullName");
            Session["IdSalon"] = Request["Id"];
            return View();
        }

        [HttpPost, ActionName("AgregarJugador")]
        public ActionResult AgregarJugador(tbl_Jugadores jugadores)
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

            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

            var IDJugador = int.Parse(Request["Id"]);
            if (IDJugador == 0)
            {
                var ListPre = db.tbl_Jugadores.ToList();
                ListPre = ListPre.OrderBy(c => c.Nombres).ToList();
                ViewBag.Id = new SelectList(ListPre, "Id", "FullName");
                ViewBag.Error = "Debe seleccionar Jugador";
                return View(jugadores);
            }
            var Jugador = db.tbl_Jugadores.Find(IDJugador);

            tbl_Jugadores JugadorE = JugadorSalonView.Jugadores.Find(p => p.Id == IDJugador);

            if (JugadorE == null)
            {

                var tbl_Jugadores = new tbl_Jugadores
                {
                    Id = int.Parse(Request["Id"]),
                    Nombres = Jugador.Nombres,
                    Apellidos=jugadores.Apellidos
                };
                JugadorSalonView.Jugadores.Add(tbl_Jugadores);
            }

            var ListPre2 = db.tbl_Jugadores.ToList();
            ListPre2 = ListPre2.OrderBy(c => c.Nombres).ToList();
            ViewBag.Id = new SelectList(ListPre2, "Id", "FullName");

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");

            return View("Agregar", JugadorSalonView);
        }

        public ActionResult AgregarJugadorEdit()
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
            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;
            var ListPre = db.tbl_Jugadores.ToList();
            ListPre = ListPre.OrderBy(c => c.Nombres).ToList();
            foreach (var item in JugadorSalonView.Jugadores)
            {
                ListPre = ListPre.Where(x => x.Id != item.Id).ToList();
            }
            ViewBag.Id = new SelectList(ListPre, "Id", "FullName");
            if (ListPre.Count() == 0)
            {
                ViewBag.Error = "No hay jugadores para agregar";
            }
            return View();
        }

        [HttpPost, ActionName("AgregarJugadorEdit")]
        public ActionResult AgregarJugadorEdit(tbl_Jugadores jugadores)
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

            var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;
            if (Request["Id"] != null)
            {
                var IDJugador = int.Parse(Request["Id"]);
            if (IDJugador == 0)
            {
                var ListPre = db.tbl_Jugadores.ToList();
                ListPre = ListPre.OrderBy(c => c.Nombres).ToList();
                ViewBag.Id = new SelectList(ListPre, "Id", "FullName");
                ViewBag.Error = "Debe seleccionar Jugador";
                return View(jugadores);
            }
            var Jugador = db.tbl_Jugadores.Find(IDJugador);

            tbl_Jugadores JugadorE = JugadorSalonView.Jugadores.Find(p => p.Id == IDJugador);

            if (JugadorE == null)
            {

                var tbl_Jugadores = new tbl_Jugadores
                {
                    Id = int.Parse(Request["Id"]),
                    Nombres = Jugador.Nombres,
                    Apellidos = jugadores.Apellidos
                };
                JugadorSalonView.Jugadores.Add(tbl_Jugadores);
            }

            var ListPre2 = db.tbl_Jugadores.ToList();
            ListPre2 = ListPre2.OrderBy(c => c.Nombres).ToList();
            ViewBag.Id = new SelectList(ListPre2, "Id", "FullName");

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");
            }
            return View("EditarSalon", JugadorSalonView);
        }

        public ActionResult Eliminar(int? id)
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

            tbl_Jugadores Jugador = db.tbl_Jugadores.Find(id);
            if (Jugador == null)
            {
                return HttpNotFound();
            }

            return View(Jugador);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Eliminar")]
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

            try
            {
                var idS = Session["IdSalon"];
                var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

                tbl_Jugadores tbl_Jugadores = JugadorSalonView.Jugadores.Find(p => p.Id == id);
                JugadorSalonView.Jugadores.Remove(tbl_Jugadores);
                var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ViewBag.IdSalon = new SelectList(listC, "Id", "Id", idS);

                return View("Agregar", JugadorSalonView);
            }
            catch (Exception ex)
            {


            }
            
            return RedirectToAction("Index");
        }

        public ActionResult EliminarEdit(int? id)
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

            tbl_Jugadores Jugador = db.tbl_Jugadores.Find(id);
            if (Jugador == null)
            {
                return HttpNotFound();
            }

            return View(Jugador);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("EliminarEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEdit(int id)
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

            try
            {
                var idS = Session["IdSalon"];
                var JugadorSalonView = Session["JugadorSalonView"] as JugadoresSalonesView;

                tbl_Jugadores tbl_Jugadores = JugadorSalonView.Jugadores.Find(p => p.Id == id);
                JugadorSalonView.Jugadores.Remove(tbl_Jugadores);
                var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ViewBag.IdSalon = new SelectList(listC, "Id", "Id", idS);

                return View("EditarSalon", JugadorSalonView);
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