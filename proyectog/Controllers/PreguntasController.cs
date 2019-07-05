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
using System.Data.SqlClient;
using ProyectoG.ViewModel;

namespace ProyectoG.Controllers
{
    public class PreguntasController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Preguntas
        public ActionResult Index()
        {
            //if (Request.Cookies["RolCookie"] == null)
            //    return RedirectToAction("Login", "Registro");
            //else
            //{
            //    var idC = Request.Cookies["RolCookie"].Value;
            //    var c = new HttpCookie("RolCookie");
            //    c.Value = idC;
            //    c.Expires = DateTime.Now.AddMinutes(15);
            //    Response.Cookies.Add(c);
            //}

            var tbl_Preguntas = db.tbl_Preguntas.Include(t => t.tbl_Categorias).Include(t => t.tbl_Niveles);
            return View(tbl_Preguntas.ToList().Where(x=> x.IdAdministrador==Convert.ToInt32( Request.Cookies["RolCookie"].Value)));
        }
        [HttpPost]
        public ActionResult Index(PreguntasRespuestasView PreguntaRView)
        {
            var tbl_Preguntas = db.tbl_Preguntas.Include(t => t.tbl_Categorias).Include(t => t.tbl_Niveles);
            var Orden = Request["Orden"];
           
            switch (Orden)
            {
                case "Nivel":
                    var Preguntas = tbl_Preguntas.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value)).OrderBy(x=> x.tbl_Niveles.Nivel);
                    return View(Preguntas);
                    break;
                case "Categoria":
                    var PreguntasC = tbl_Preguntas.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value)).OrderBy(x => x.tbl_Categorias.Categoria);
                    return View(PreguntasC);
                    break;
                case "0":
                    var PreguntasS = tbl_Preguntas.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
                    return View(PreguntasS);
                    break;

            }
            return View(PreguntaRView);




        }

        // GET: Preguntas/Details/5
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

            var Pregunta = db.tbl_Preguntas.Find(id);
            if (Pregunta == null)
            {
                return HttpNotFound();
            }

            var Respuesta = db.tbl_Respuestas.SqlQuery("sp_ConsultaPreguntasDetallado @IDP", new SqlParameter("@IDP", id)).ToList();

            return View(new PreguntasRespuestasView
            {
                Respuestas = Respuesta,
                Preguntas = Pregunta
            });
        }

        // GET: Preguntas/Create
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

            var PreguntaRView = new PreguntasRespuestasView();
            PreguntaRView.Preguntas = new tbl_Preguntas();
            PreguntaRView.Respuestas = new List<tbl_Respuestas>();


            var ListCat = db.tbl_Categorias.ToList();
            ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
            ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
            ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria");
            var ListNi = db.tbl_Niveles.ToList();
            ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
            ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
            ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");
            return View(PreguntaRView);
        }

        // POST: Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PreguntasRespuestasView PreguntaRView)
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


            var CategoriaID = int.Parse(Request["Id_Categoria"]);
            var NivelID = int.Parse(Request["Id_Nivel"]);
            var Link = Request["Preguntas.Link"].ToString();


            if (CategoriaID == 0)
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria");
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");
                ViewBag.Error = "Debe seleccionar una categoria";
                return View(PreguntaRView);
            }
            if (NivelID == 0)
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");
                ViewBag.Error = "Debe seleccionar un nivel";
                return View(PreguntaRView);
            }
            var Pregunta = Request["Preguntas.Pregunta"].ToString();
            if (Pregunta=="")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel",NivelID);
                return View(PreguntaRView);
            }
            if (Link == "")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                return View(PreguntaRView);
            }

            var Respuesta1 = Request["Respuesta1.Respuesta"].ToString();
            if(Respuesta1=="")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                return View(PreguntaRView);
            }
            var Respuesta2 = Request["Respuesta2.Respuesta"].ToString();
            if(Respuesta2=="")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                return View(PreguntaRView);
            }
            var Respuesta3 = Request["Respuesta3.Respuesta"].ToString();
            if(Respuesta3=="")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                return View(PreguntaRView);
            }
            int sw = 0;
            for (int i = 1; i < 4; i++)
            {
                var Respuesta_Correcta = Request["Respuesta" + i + ".Respuesta_Correcta"].ToString();
                if (sw == 1 & Respuesta_Correcta != "false")
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                    ViewBag.Error = "Seleccione sólo una respuesta correcta";

                    return View(PreguntaRView);
                }
                if (sw == 0 & Respuesta_Correcta != "false")
                    sw = 1;
            }
            sw = 0;
            for (int i = 1; i < 4; i++)
            {
               
                var Respuesta_Correcta = Request["Respuesta" + i + ".Respuesta_Correcta"].ToString();
                if (sw == 0 & Respuesta_Correcta != "false")
                    sw = 1;
                
                if ((sw == 0 & i == 3))
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                    ViewBag.Error = "Seleccione una respuesta correcta";

                    return View(PreguntaRView);
                }
            }
            int IDPregunta = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var Privada = Request["Preguntas.Privada"].ToString();
                    if (Privada != "false")
                        Privada = "true";
                    
                   

                    var Resp = db.tbl_Preguntas.SqlQuery("sp_InsertarPregunta @Id_Categoria,@Id_Nivel,@Pregunta,@Link,@IdAdministrador,@Privada", new SqlParameter("@Id_Categoria", CategoriaID),
                                                                                    new SqlParameter("@Id_Nivel", NivelID),
                                                                                    new SqlParameter("@Pregunta", Request["Preguntas.Pregunta"].ToString()),
                                                                                    new SqlParameter("@Link", Link),
                                                                                    new SqlParameter("@IdAdministrador", int.Parse(Request.Cookies["RolCookie"].Value)),
                                                                                    new SqlParameter("@Privada", bool.Parse(Privada))).ToList();

                    IDPregunta = db.tbl_Preguntas.ToList().Select(p => p.Id).Max();
                   sw = 0;

                    for (int i = 1; i < 4; i++)
                    {
                        var Respuesta = Request["Respuesta" + i + ".Respuesta"].ToString();
                        var Respuesta_Correcta = Request["Respuesta" + i + ".Respuesta_Correcta"].ToString();
                        if (sw == 0 & Respuesta_Correcta != "false")
                            sw = 1;
                        else if ((sw == 1 & Respuesta_Correcta != "false") || (sw == 0 & i == 3))
                        {
                            var ListCat = db.tbl_Categorias.ToList();
                            ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                            ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                            ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria",CategoriaID);
                            var ListNi = db.tbl_Niveles.ToList();
                            ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                            ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                            ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel",NivelID);
                            ViewBag.Error = "Seleccione una respuesta correcta";
                            Transaction.Rollback();
                            return View(PreguntaRView);
                        }

                        if (Respuesta_Correcta != "false")
                            Respuesta_Correcta = "true";
                        var Respuestas = new tbl_Respuestas
                        {
                            Id_Pregunta = IDPregunta,
                            Respuesta = Respuesta,
                            Respuesta_Correcta = bool.Parse(Respuesta_Correcta)
                        };

                        db.tbl_Respuestas.Add(Respuestas);
                        db.SaveChanges();

                    }

                    Transaction.Commit();


                }
                catch (Exception ex)
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria");
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");

                    Transaction.Rollback();
                    ViewBag.Error = "Error: " + ex.Message;

                    return View(PreguntaRView);
                }
            }


            return RedirectToAction("Index");


        }

        // GET: Preguntas/Edit/5
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

            var PreguntaRView = new PreguntasRespuestasView();
            PreguntaRView.Preguntas = new tbl_Preguntas();
            PreguntaRView.Respuestas = new List<tbl_Respuestas>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["Id"] = id;
            tbl_Preguntas tbl_Preguntas = db.tbl_Preguntas.Find(id);
            if (tbl_Preguntas == null)
            {
                return HttpNotFound();
            }
            var Respuesta = db.tbl_Respuestas.SqlQuery("sp_ConsultaPreguntasDetallado @IDP", new SqlParameter("@IDP", id)).ToList();

            ViewBag.Id_Categoria = new SelectList(db.tbl_Categorias, "Id", "Categoria", tbl_Preguntas.Id_Categoria);
            ViewBag.Id_Nivel = new SelectList(db.tbl_Niveles, "Id_Nivel", "Nivel", tbl_Preguntas.Id_Nivel);

             PreguntaRView = new PreguntasRespuestasView
            {
                Respuestas = Respuesta,
                Preguntas = tbl_Preguntas
            };

            Session["RespuestasEdit"] = Respuesta;
            return View(PreguntaRView);



        }

        // POST: Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PreguntasRespuestasView PreguntaRView)
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

            List<tbl_Respuestas> Respuestas = Session["RespuestasEdit"] as List<tbl_Respuestas>;


                int Id = Convert.ToInt32(Session["Id"]);
                var CategoriaID = int.Parse(Request["Id_Categoria"]);
                var NivelID = int.Parse(Request["Id_Nivel"]);
                var Pregunta = Request["Preguntas.Pregunta"].ToString();
                var Link = Request["Preguntas.Link"].ToString();
            var Privada = Request["Preguntas.Privada"].ToString();
            if (Privada == "false")
                Privada = "0";
            else
                Privada = "1";

            if (CategoriaID == 0)
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria");
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");
                    ViewBag.Error = "Debe seleccionar una categoria";
                    PreguntaRView.Respuestas = Respuestas;
                    return View(PreguntaRView);
                }
                if (NivelID == 0)
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel");
                    ViewBag.Error = "Debe seleccionar un nivel";
                    PreguntaRView.Respuestas = Respuestas;
                return View(PreguntaRView);
                }
                if (Pregunta == "")
                {
                    var ListCat = db.tbl_Categorias.ToList();
                    ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                    ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                    ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                    var ListNi = db.tbl_Niveles.ToList();
                    ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                    ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                    ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                    PreguntaRView.Respuestas = Respuestas;
                return View(PreguntaRView);
                }
            if (Link == "")
            {
                var ListCat = db.tbl_Categorias.ToList();
                ListCat.Add(new tbl_Categorias { Id = 0, Categoria = "[Seleccione Categoria...]" });
                ListCat = ListCat.OrderBy(c => c.Categoria).ToList();
                ViewBag.Id_Categoria = new SelectList(ListCat, "Id", "Categoria", CategoriaID);
                var ListNi = db.tbl_Niveles.ToList();
                ListNi.Add(new tbl_Niveles { Id_Nivel = 0, Nivel = "[Seleccione Nivel...]" });
                ListNi = ListNi.OrderBy(n => n.Nivel).ToList();
                ViewBag.Id_Nivel = new SelectList(ListNi, "Id_Nivel", "Nivel", NivelID);
                PreguntaRView.Respuestas = Respuestas;
                return View(PreguntaRView);
            }
            using (var Transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                    db.tbl_Preguntas.SqlQuery
                        ("sp_ModificarPregunta @IDP,@Pregunta,@IdCategoria,@IdNivel,@Link,@Privada",
                        new SqlParameter("@IDP", Id), new SqlParameter("@Pregunta", Pregunta),
                        new SqlParameter("@IdCategoria", CategoriaID), new SqlParameter("@IdNivel", NivelID),
                        new SqlParameter("@Link", Link), new SqlParameter("@Privada", Privada)).ToList();
                        Transaction.Commit();
                  

                }
                    catch (Exception ex)
                    {
                        ViewBag.Id_Categoria = new SelectList(db.tbl_Categorias, "Id", "Categoria", CategoriaID);
                        ViewBag.Id_Nivel = new SelectList(db.tbl_Niveles, "Id_Nivel", "Nivel", NivelID);
                        Transaction.Rollback();
                        ViewBag.Error = "Error: " + ex.Message;

                        return View(PreguntaRView);
                    }
                }

                return RedirectToAction("Index");
          
        }

        // GET: Preguntas/Delete/5
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

            var Pregunta = db.tbl_Preguntas.Find(id);
            if (Pregunta == null)
            {
                return HttpNotFound();
            }

            var Respuesta = db.tbl_Respuestas.SqlQuery("sp_ConsultaPreguntasDetallado @IDP", new SqlParameter("@IDP", id)).ToList();

            return View(new PreguntasRespuestasView
            {
                Respuestas = Respuesta,
                Preguntas = Pregunta
            });
        }

        // POST: Preguntas/Delete/5
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

            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var Respuesta = db.tbl_Respuestas.SqlQuery("sp_ConsultaPreguntasDetallado @IDP", new SqlParameter("@IDP", id)).ToList();

                    foreach (var item in Respuesta)
                    {
                        tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(item.Id);
                        db.tbl_Respuestas.Remove(tbl_Respuestas);
                        db.SaveChanges();
                    }

                    tbl_Preguntas tbl_Preguntas = db.tbl_Preguntas.Find(id);
                    db.tbl_Preguntas.Remove(tbl_Preguntas);
                    db.SaveChanges();

                  
                    Transaction.Commit();
                }
                catch (Exception ex)
                {

                    Transaction.Rollback();
                }
            }



            return RedirectToAction("Index");
        }

        public ActionResult EliminarRespuesta(int? id)
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
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            if (tbl_Respuestas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Respuestas);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("EliminarRespuesta")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedR(int id)
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

            int IdP = Convert.ToInt32(Session["Id"]);
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            db.tbl_Respuestas.Remove(tbl_Respuestas);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = IdP });
        }

        public ActionResult EditarRespuesta(int? id)
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
            tbl_Respuestas tbl_Respuestas = db.tbl_Respuestas.Find(id);
            if (tbl_Respuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pregunta = new SelectList(db.tbl_Preguntas, "Id", "Pregunta", tbl_Respuestas.Id_Pregunta);
            return View(tbl_Respuestas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarRespuesta([Bind(Include = "Id,Id_Pregunta,Respuesta,Respuesta_Correcta")] tbl_Respuestas tbl_Respuestas)
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
                int IdP = Convert.ToInt32(Session["Id"]);
                db.Entry(tbl_Respuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = IdP });
            }
           
            return View(tbl_Respuestas);
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
