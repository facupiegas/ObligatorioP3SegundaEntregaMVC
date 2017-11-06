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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (HomeController.ValidarUsuario(usuario.Nombre,usuario.Pass))
                {
                    FormsAuthentication.SetAuthCookie(usuario.Nombre,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Alguno de los datos es incorrecto. Vuelva a intentarlo.");
                }
            }
            return View(usuario);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            return RedirectToAction("Login", "Home");
        }

        public static bool ValidarUsuario(string nombre, string pass)
        {
            bool retorno = false;
            using (GestionEventosContext db = new GestionEventosContext())
            {
                Usuario tmpUsuario = db.Usuarios.Find(nombre);
                if (tmpUsuario != null)
                {
                    string sal = tmpUsuario.Sal;
                    if (Usuario.GenerarSHA256Hash(pass, sal) == tmpUsuario.Pass)
                    {
                        retorno = true;
                    }
                }

            }
            return retorno;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}