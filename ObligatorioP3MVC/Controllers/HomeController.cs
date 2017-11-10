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
            //en caso de no loguearse con las credenciales adecuadas se muestra un mensaje de error
            ViewBag.MotivoCierreSesion = mensaje;
            return View();

        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (this.ValidarUsuario(usuario.Nombre, usuario.Pass))
                {
                    Session["UsuarioLogueado"] = usuario.Nombre;
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

        public bool ValidarUsuario(string nombre, string pass)
        {
            bool retorno = false;
            using (GestionEventosContext db = new GestionEventosContext())
            {
                //busco el usuario con el nombre ingresado en la bd
                Usuario tmpUsuario = db.Usuarios.Find(nombre);
                if (tmpUsuario != null)
                {
                    //recupero la sal del usuario guardado
                    string sal = tmpUsuario.Sal;
                    //genero una password con la contraseña ingresada por parametro y la sal del usuario guardado en la base y compruebo con la password del usuario
                    if (Usuario.GenerarSHA256Hash(pass, sal) == tmpUsuario.Pass) 
                    {
                        //guardo el rol del Usuario autenticado
                        Session["TipoDeUsuario"] = tmpUsuario.Rol.ToString(); 
                        retorno = true;
                        if (nombre.Contains("@")) //si el nombre contiene @ quiere decir que es un usuario con rol de Organizador
                        {
                            Organizador org = db.Organizadores.Where(p => p.Usuario.Nombre == nombre).First();
                            Session["OrganizadorLogueado"] = org.NombreOrganizador.ToString();
                        }
                        
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