using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoG.Basedatos;
using System.Data;
using ProyectoG.ViewModel;
using ProyectoG.Models;
using ProyectoG.Context;

namespace ProyectoG.Controllers
{
    public class InformesController : Controller
    {
        BaseDatos bd = new BaseDatos();
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

            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "IdAcceso");
            InformesViewModel InformesView = new InformesViewModel();
            InformesView.Tipo_Informe = "";
            return View(InformesView);
        }
      [HttpPost]
        public ActionResult Index(InformesViewModel InformesView)
        {
            
            var IdSalon = Request["IdSalon"];
            DataTable Datos = new DataTable();
            var TipoInforme = Request["TipoInforme"];
            InformesView.Informes = new List<Informes>();
            switch (TipoInforme)
            {
                case "1":
                    Datos = bd.ObtenerDatos(GetQueryTiempoPromedio(IdSalon));
                    foreach (DataRow negocio in Datos.Rows)
                    {
                        InformesView.Informes.Add(new Informes()
                        {
                            Tiempo_Promedio =Convert.ToInt32(negocio["Tiempo_Promedio"]),
                            Pregunta = negocio["Pregunta"].ToString()

                        });
                    }
                    break;
                case "2":
                     Datos = bd.ObtenerDatos(GetQueryCantidadCorrectas(IdSalon));
                    foreach (DataRow negocio in Datos.Rows)
                    {
                        InformesView.Informes.Add(new Informes()
                        {
                            Pregunta = negocio["Pregunta"].ToString(),
                            Correctas = Convert.ToInt32( negocio["Correctas"]),
                             Incorrectas= Convert.ToInt32(negocio["Incorrectas"])

                        });
                    }
                    break;

                case "3":
                    Datos = bd.ObtenerDatos(GetQueryDesempeño(IdSalon));
                    foreach (DataRow negocio in Datos.Rows)
                    {
                        InformesView.Informes.Add(new Informes()
                        {
                            Jugador = negocio["Jugador"].ToString(),
                            Correctas = Convert.ToInt32(negocio["Correctas"]),
                            Incorrectas = Convert.ToInt32(negocio["Incorrectas"]),
                            Nota= Convert.ToDecimal( negocio["Nota"]),
                            Fecha_Calificacion= Convert.ToDateTime(negocio["Fecha_Calificacion"])

                        });
                    }
                    break;
            }
            
            var listC = db.tbl_Salones.ToList().Where(x => x.IdAdministrador == Convert.ToInt32(Request.Cookies["RolCookie"].Value));
            ViewBag.IdSalon = new SelectList(listC, "Id", "IdAcceso");
            InformesView.Tipo_Informe = TipoInforme;
           

            return View(InformesView);
           
        }

        private string GetQueryTiempoPromedio(string IdSalon)
        {
            return string.Format(@"select pr.pregunta 'Pregunta',(avg(enc.tiempo)/1000) 'Tiempo_Promedio'
                                    from tbl_informessalones enc inner join
                                    tbl_jugadores ju on ju.Id=enc.idjugador inner join
                                    tbl_salones sa on sa.id=enc.idsalon inner join 
                                    tbl_preguntas pr on pr.id=enc.idpregunta
                                    where sa.id={0}
                                    group by pr.pregunta",IdSalon);
        }
        private string GetQueryCantidadCorrectas(string IdSalon)
        {
            return string.Format(@" select pr.pregunta 'Pregunta',
			                        (select count(sub.corecta)

                                    from tbl_informessalones sub
                                    where corecta=1 and idsalon = sa.id and sub.idpregunta=pr.id) 'Correctas',
			                        (select count(sub2.corecta)

                                    from tbl_informessalones sub2
                                    where corecta=0 and idsalon = sa.id and sub2.idpregunta=pr.id) 'Incorrectas'
                                                            from tbl_informessalones enc inner join
                                                            tbl_salones sa on sa.id=enc.idsalon inner join
                                                            tbl_preguntas pr on pr.id=enc.idpregunta
                                                            where sa.id={0}
                                                            group by pr.pregunta,pr.id,sa.id", IdSalon);
        }

        private string GetQueryDesempeño(string IdSalon)
        {
            return string.Format(@"select (ju.nombres+' '+ ju.apellidos) 'Jugador', enc.correctas 'Correctas',
                                    enc.incorrectas 'Incorrectas', enc.nota 'Nota',
                                    enc.fechacalificacion 'Fecha_Calificacion'
                                    from tbl_puntuacionsalones enc inner join
                                    tbl_jugadores ju on ju.Id=enc.idjugador inner join
                                    tbl_salones sa on sa.idacceso=enc.idacceso 
                                    where sa.id={0}", IdSalon);
        }

    }
}