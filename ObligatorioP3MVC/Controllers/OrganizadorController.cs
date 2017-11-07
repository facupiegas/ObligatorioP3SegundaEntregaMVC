using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3MVC.Models;

namespace ObligatorioP3MVC.Controllers
{
    public class OrganizadorController : Controller
    {
        public ActionResult Error(string mensaje)
        {
            if (mensaje == null || mensaje =="") mensaje = "No hay detalle para el Error";
            ViewBag.Error =mensaje;
            return View();
        }


        [HttpGet]
        public ActionResult Crear()
        {
            Organizador org = new Organizador();
            return View(org);
        }
        [HttpPost]
        public ActionResult Crear(Organizador org)
        {
            var parametroDeAccion = (Object)null;
            string accion = string.Empty;
            if (ModelState.IsValid)
            {
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    if (db.Organizadores.Find(org.NombreOrganizador) == null)
                    {
                        if (db.Usuarios.Find(org.Email) == null)
                        {
                            Usuario tmpUser = new Usuario(org.Email, org.Usuario.Pass, Usuario.EnumRol.Organizador);
                            org.Usuario = tmpUser;
                            db.Organizadores.Add(org);
                            db.SaveChanges();
                            accion = "Datos";
                            parametroDeAccion = new { nombre = org.NombreOrganizador };
                            TempData["Organizador"] = org;
                        }
                        else
                        {
                            accion = "Error";
                            parametroDeAccion = new { mensaje = "Ya existe un Usuario con el Email ingresado" };
                        }
                    }
                    else
                    {
                        accion = "Error";
                        parametroDeAccion = new { mensaje = "Ya existe un Organizador con el Nombre ingresado" };
                    }
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction(accion, parametroDeAccion);
        }

        [HttpGet]
        public ActionResult Editar()
        {
            Organizador org = new Organizador();
            return View(org);
        }
        [HttpPost]
        public ActionResult Editar(Organizador org)
        {
            var parametroDeAccion = (Object)null;
            string accion = string.Empty;
            if (org != null)
            {
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    Organizador auxOrg = db.Organizadores.Find(org.NombreOrganizador);
                    if (auxOrg != null)
                    {
                        auxOrg.Telefono = org.Telefono;
                        db.SaveChanges();
                        accion = "Datos";
                        TempData["Organizador"] = auxOrg;
                    }
                    else
                    {
                        accion = "Error";
                        parametroDeAccion = new { mensaje = "" };
                    }
                }
            }
            return RedirectToAction(accion, parametroDeAccion);
        }

        public ActionResult Datos()
        {
            Organizador tmp = null;
            if (TempData["Organizador"] != null)
            {
                tmp = (Organizador)TempData["Organizador"];
                TempData["Organizador"] = tmp;
            }
            return View(tmp);
        }

        public ActionResult Listar()
        {
            var lista = new List<Organizador>();
            using (GestionEventosContext db = new GestionEventosContext())
            {
                lista = db.Organizadores.ToList();
            }
            return View(lista);
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