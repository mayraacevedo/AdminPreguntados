using ProyectoG.Context;
using ProyectoG.Models; 
using ProyectoG.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProyectoG.Controllers
{
    public class PreguntasSalonesController : Controller
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

            return View(db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value)));
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

            var PreguntaRView = new PreguntasSalonesView();
            PreguntaRView.Preguntas = new List<tbl_Preguntas>();
            PreguntaRView.Respuestas = new List<tbl_Respuestas>();

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");

            Session["PreguntaSalonView"] = PreguntaRView;
            return View(PreguntaRView);
        }


        [HttpPost, ActionName("Agregar")]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(PreguntasRespuestasView PreguntaRView)
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

            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;

            var IdSalon = Request["IdSalon"];


            if (IdSalon == "")
            {
                ViewBag.Error = "Debe ingresar un id de salon";
                return View(PreguntaSalonView);
            }

            var Salones = db.tbl_Salones.SqlQuery("sp_ConsultarSalon @ID", new SqlParameter("@ID", IdSalon)).ToList();

            if (Salones.Count() == 0)
            {
                ViewBag.Error = "El id de salon no existe";
                return View(PreguntaSalonView);
            }
            if (PreguntaSalonView.Preguntas.Count == 0)
            {
                ViewBag.Error = "Debe ingresar Preguntas";
                return View(PreguntaSalonView);
            }

            foreach (var item in PreguntaSalonView.Preguntas)
            {
                var Respuesta = db.tbl_PreguntaSalones.SqlQuery("sp_InsertarPreguntasSalon @IdP,@IdS", new SqlParameter("@IdP", item.Id), new SqlParameter("@IdS", IdSalon)).ToList();
            }

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ViewBag.Id = new SelectList(listC, "Id", "Id");
            return RedirectToAction("Index");

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

            var PreguntaRView = new PreguntasSalonesView();
            PreguntaRView.Preguntas = new List<tbl_Preguntas>();
            PreguntaRView.Respuestas = new List<tbl_Respuestas>();
            PreguntaRView.Preguntas = db.tbl_Preguntas.SqlQuery("sp_ConsultaPreguntaSalon @IdS", new SqlParameter("@IdS", Id)).ToList();

            Session["IdSalon"] = Id;
            Session["PreguntaSalonView"] = PreguntaRView;
            return View(PreguntaRView);
        }

        [HttpPost, ActionName("EditarSalon")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarSalon(PreguntasRespuestasView PreguntaRView)
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

            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;

            var IdSalon = Session["IdSalon"];


            if (IdSalon == "")
            {
                ViewBag.Error = "Debe ingresar un id de salon";
                return View(PreguntaSalonView);
            }

            var Salones = db.tbl_Salones.SqlQuery("sp_ConsultarSalon @ID", new SqlParameter("@ID", IdSalon)).ToList();

            if (Salones.Count() == 0)
            {
                ViewBag.Error = "El id de salon no existe";
                return View(PreguntaSalonView);
            }
            if (PreguntaSalonView.Preguntas.Count == 0)
            {
                ViewBag.Error = "Debe ingresar Preguntas";
                return View(PreguntaSalonView);
            }

            var Lista = db.tbl_PreguntaSalones.SqlQuery("sp_EliminarPreguntaSalon @IdS", new SqlParameter("@IdS", IdSalon)).ToList();
            foreach (var item in PreguntaSalonView.Preguntas)
            {
                var Respuesta = db.tbl_PreguntaSalones.SqlQuery("sp_InsertarPreguntasSalon @IdP,@IdS", new SqlParameter("@IdP", item.Id), new SqlParameter("@IdS", IdSalon)).ToList();

            }


            return RedirectToAction("Index");

        }
        public ActionResult AgregarPreguntaEdit()
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
            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;
           
            var ListPre = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ListPre = ListPre.OrderBy(c => c.Pregunta).ToList();
            foreach (var item in PreguntaSalonView.Preguntas)
            {
                ListPre = ListPre.Where(x => x.Id != item.Id);
            }
            ViewBag.Id = new SelectList(ListPre, "Id", "Pregunta");

            return View();
        }

        [HttpPost, ActionName("AgregarPreguntaEdit")]
        public ActionResult AgregarPreguntaEdit(PreguntasRespuestasView PreguntaRView)
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

            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;

            var IDPregunta = int.Parse(Request["Id"]);
            if (IDPregunta == 0)
            {
                var ListPre = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ListPre = ListPre.OrderBy(c => c.Pregunta).ToList();
                ViewBag.Id = new SelectList(ListPre, "Id", "Pregunta");
                ViewBag.Error = "Debe seleccionar pregunta";
                return View(PreguntaRView);
            }
            var Pregunta = db.tbl_Preguntas.Find(IDPregunta);

            tbl_Preguntas PreguntaE = PreguntaSalonView.Preguntas.Find(p => p.Id == IDPregunta);

            if (PreguntaE == null)
            {

                var tbl_Preguntas = new tbl_Preguntas
                {
                    Id = int.Parse(Request["Id"]),
                    Pregunta = Pregunta.Pregunta
                };
                PreguntaSalonView.Preguntas.Add(tbl_Preguntas);
            }

            var ListPre2 = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ListPre2 = ListPre2.OrderBy(c => c.Pregunta).ToList();
            ViewBag.Id = new SelectList(ListPre2, "Id", "Pregunta");

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");

            return View("EditarSalon", PreguntaSalonView);
        }

        public ActionResult AgregarPregunta()
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
            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;
            var ListPre = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ListPre = ListPre.OrderBy(c => c.Pregunta).ToList();
            foreach (var item in PreguntaSalonView.Preguntas)
            {
                ListPre = ListPre.Where(x => x.Id != item.Id);
            }
            ViewBag.Id = new SelectList(ListPre, "Id", "Pregunta");
            if (ListPre.Count() == 0)
            {
                ViewBag.Error = "No hay preguntas para agregar";
            }
            return View();
        }

        [HttpPost, ActionName("AgregarPregunta")]
        public ActionResult AgregarPregunta(PreguntasRespuestasView PreguntaRView)
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

            var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;
            if(Request["Id"]!=null)
            { 
            var IDPregunta = int.Parse(Request["Id"]);
            
            if (IDPregunta == 0)
            {
                var ListPre = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ListPre = ListPre.OrderBy(c => c.Pregunta).ToList();
                ViewBag.Id = new SelectList(ListPre, "Id", "Pregunta");
                ViewBag.Error = "Debe seleccionar pregunta";
                return View(PreguntaRView);
            }
            var Pregunta = db.tbl_Preguntas.Find(IDPregunta);

            tbl_Preguntas PreguntaE = PreguntaSalonView.Preguntas.Find(p => p.Id == IDPregunta);

            if (PreguntaE == null)
            {

                var tbl_Preguntas = new tbl_Preguntas
                {
                    Id = int.Parse(Request["Id"]),
                    Pregunta = Pregunta.Pregunta
                };
                PreguntaSalonView.Preguntas.Add(tbl_Preguntas);
            }

            var ListPre2 = db.tbl_Preguntas.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ListPre2 = ListPre2.OrderBy(c => c.Pregunta).ToList();
            ViewBag.Id = new SelectList(ListPre2, "Id", "Pregunta");

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "Id");
            }
            return View("Agregar", PreguntaSalonView);
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

            tbl_Preguntas Pregunta = db.tbl_Preguntas.Find(id);
            if (Pregunta == null)
            {
                return HttpNotFound();
            }

            return View(Pregunta);
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
                var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;

                tbl_Preguntas tbl_Preguntas = PreguntaSalonView.Preguntas.Find(p => p.Id == id);
                PreguntaSalonView.Preguntas.Remove(tbl_Preguntas);

                var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ViewBag.IdSalon = new SelectList(listC, "Id", "Id");

                return View("Agregar", PreguntaSalonView);
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

            tbl_Preguntas Pregunta = db.tbl_Preguntas.Find(id);
            if (Pregunta == null)
            {
                return HttpNotFound();
            }

            return View(Pregunta);
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
                var PreguntaSalonView = Session["PreguntaSalonView"] as PreguntasSalonesView;

                tbl_Preguntas tbl_Preguntas = PreguntaSalonView.Preguntas.Find(p => p.Id == id);
                PreguntaSalonView.Preguntas.Remove(tbl_Preguntas);

                var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador ==Convert.ToInt32( Request.Cookies["RolCookie"].Value));
                ViewBag.IdSalon = new SelectList(listC, "Id", "Id");

                return View("EditarSalon", PreguntaSalonView);
            }
            catch (Exception ex)
            {


            }

            return RedirectToAction("Index");
        }

        public JsonResult ConsultarPregunta(int ID)
        {

            return Json(db.tbl_Respuestas.Where(a => a.Id_Pregunta == ID), JsonRequestBehavior.AllowGet);
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