using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProyectoG.Context;
using ProyectoG.Models;
using System.Data.SqlClient;
using System.Net.Mail;

namespace ProyectoG.Controllers
{
    public class RegistroController : Controller
    {
        private GameContext db = new GameContext();
        
       
        public ActionResult Create()
        {
            ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre");
           
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(tbl_Administradores tbl_Administradores)
        {
                var Correo = Request["Correo"].ToString();
                tbl_Administradores.Rol = "Docente";
                
                if (!Correo.Contains(".edu"))
                {
                    ViewBag.Error = "El correo debe ser un .edu";
                    ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre");
                    var values = from Roles e in Enum.GetValues(typeof(Roles))
                                 select new { Id = e, Name = e.ToString() };
                    //ViewBag.EnumList = new SelectList(values, "ID", "Name");
                    return View();

                }
                else
                {
                    db.tbl_Administradores.Add(tbl_Administradores);
                    db.SaveChanges();
                    ViewBag.Error = "Usuario creado con éxito, puede ingresar con su correo y clave";
                ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
                return View();
                }
            
        }

        
        public ActionResult Login()
        {
            if (Request.Cookies["RolCookie"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            //SendEmail();
            return View();
        }

     
        [HttpPost]
        public ActionResult Login(tbl_Administradores model)
        {
            
                var Correo = Request["Correo"].ToString();
                var Clave = Request["Clave"];

            var Resultado = db.tbl_Administradores.ToList().Where(x => x.Correo == Correo && x.Clave == Clave);

            if (Resultado.Count() > 0) {
                HttpCookie RolCookies = new HttpCookie("RolCookie");
                RolCookies.Value = Resultado.ElementAt(0).Id.ToString();
                RolCookies.Expires = DateTime.Now.AddMinutes(15);
                this.ControllerContext.HttpContext.Response.Cookies.Add(RolCookies);
                Session["IDUsuario"] = Resultado.ElementAt(0).Id.ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Error = "correo o clave incorrectos";
                return View(model);
            }
        }


        public ActionResult Cerrar(tbl_Administradores model)
        {
            if (Request.Cookies["RolCookie"] != null)
            {
                var c = new HttpCookie("RolCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            Session["IDUsuario"] = null;
            return RedirectToAction("Login", "Registro");

        }

        public ActionResult Edit()
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

            string id = Request.Cookies["RolCookie"].Value;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Administradores tbl_Administradores = db.tbl_Administradores.Find(Convert.ToInt32( id));
            if (tbl_Administradores == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
            return View(tbl_Administradores);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Administradores tbl_Administradores)
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
            
            string ClaveAct = Request["ClaveAct"].ToString();

            tbl_Administradores tbl_AdministradoresAct = db.tbl_Administradores.Find(tbl_Administradores.Id);

            if(ClaveAct!="")
            {
                if (tbl_AdministradoresAct.Clave.ToString() == ClaveAct)
                {
                    var Respuesta = db.tbl_Administradores.SqlQuery("sp_ModificarPerfil @Nombre, @Apellido, @FechaNacimiento,@IdIntitucion, @Clave,@ID",
                                                               new SqlParameter("@Nombre", tbl_Administradores.Nombre),
                                                               new SqlParameter("@Apellido", tbl_Administradores.Apellido),
                                                               new SqlParameter("@FechaNacimiento", tbl_Administradores.FechaNacimiento),
                                                               new SqlParameter("@IdIntitucion", tbl_Administradores.IdInstitucion),
                                                               new SqlParameter("@Clave", tbl_Administradores.Clave),
                                                               new SqlParameter("@ID", tbl_Administradores.Id)).ToList();
                    ViewBag.Error = "Perfil editado con éxto";
                    return View(tbl_Administradores);
                }
                else
                {
                    ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
                    ViewBag.Error = "Contraseña actual incorrecta";
                    return View(tbl_Administradores);
                }
            }
            else
            {

                if (tbl_AdministradoresAct.Clave.ToString() == tbl_Administradores.Clave)
                {
                    var Respuesta = db.tbl_Administradores.SqlQuery("sp_ModificarPerfil @Nombre, @Apellido, @FechaNacimiento,@IdIntitucion, @Clave,@ID",
                                                               new SqlParameter("@Nombre", tbl_Administradores.Nombre),
                                                               new SqlParameter("@Apellido", tbl_Administradores.Apellido),
                                                               new SqlParameter("@FechaNacimiento", tbl_Administradores.FechaNacimiento),
                                                               new SqlParameter("@IdIntitucion", tbl_Administradores.IdInstitucion),
                                                               new SqlParameter("@Clave", tbl_Administradores.Clave),
                                                               new SqlParameter("@ID", tbl_Administradores.Id)).ToList();
                    ViewBag.Error = "Perfil editado con éxto";
                    return View(tbl_Administradores);
                }
                else
                {
                    ViewBag.IdInstitucion = new SelectList(db.tbl_Instituciones, "IdInstitucion", "Nombre", tbl_Administradores.IdInstitucion);
                    ViewBag.Error = "No puede cambiar la contraseña sin ingresar la contraseña actual";
                    return View(tbl_Administradores);
                }
            }

            
            
            
        }

        private void SendEmail()
        {
            try
            {

                var fromAddress = new MailAddress("mayraa301@gmail.com", "From Name");
                var toAddress = new MailAddress("mayra_acevedo@hotmail.es", "To Name");
                const string fromPassword = "Miam91102454451";
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }


            //    MailMessage Correo = new MailMessage();

           
            //Correo.From= new MailAddress("mayraa301@gmail.com");
            //Correo.To.Add(new MailAddress("mayra_acevedo@hotmail.es"));
            

            //Correo.Subject = "Confirmar Correo";

            //Correo.Body = "Confirmar correo electronico";

            //Correo.Priority = MailPriority.High;


            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.EnableSsl = true;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.UseDefaultCredentials = false;

            //string CuentaCorreo = "mayraa301@gmail.com";
            //string Password = "Miam91102454451";

            //smtp.Credentials = new System.Net.NetworkCredential(CuentaCorreo, Password);

            //smtp.Send(Correo);
            }
            catch (Exception ex)
            {

                throw;
            }
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
