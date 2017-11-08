using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3MVC.Models;
using System.Web.Security;

namespace ObligatorioP3MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login(string mensaje = "")
        {
            ViewBag.MotivoCierreSesion = mensaje;
            return View();

        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string Rol = this.ValidarUsuario(usuario.Nombre, usuario.Pass);
                if (Rol == "Administrador" || Rol == "Organizador" || Rol == "Proveedor")
                {
                    Session["UsuarioLogueado"] = usuario.Nombre;
                    Session["TipoDeUsuario"] = Rol;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Alguno de los datos es incorrecto. Vuelva a intentarlo.");
                }
            }
            return View(usuario);
        }
        public ActionResult Logout(string mensaje = "")
        {

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Home",new { mensaje=mensaje});
        }

        public string ValidarUsuario(string nombre, string pass)
        {
            string retorno = "";
            using (GestionEventosContext db = new GestionEventosContext())
            {
                Usuario tmpUsuario = db.Usuarios.Find(nombre);
                if (tmpUsuario != null)
                {
                    string sal = tmpUsuario.Sal;
                    if (Usuario.GenerarSHA256Hash(pass, sal) == tmpUsuario.Pass)
                    {
                        retorno = tmpUsuario.Rol.ToString();
                        Organizador org = db.Organizadores.Where(p => p.Usuario.Nombre == nombre).First();
                        Session["OrganizadorLogueado"] = org.NombreOrganizador.ToString();
                    }
                }

            }
            return retorno;
        }

        public ActionResult Index()
        {
            if (this.esOrganizador() || this.esAdmin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
        }

        public bool esAdmin()
        {
            return Session["TipoDeUsuario"].ToString() == "Administrador" ? true : false;
        }
        public bool esOrganizador()
        {
            return Session["TipoDeUsuario"].ToString() == "Organizador" ? true : false;
        }
    }
}