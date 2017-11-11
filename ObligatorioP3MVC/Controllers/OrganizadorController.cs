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
            //Defino variables auxiliares para el retorno
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
                            //Creo el Usuario
                            Usuario tmpUser = new Usuario(org.Email, org.Usuario.Pass, Usuario.EnumRol.Organizador);
                            //Asigno el Usuario creado al Organizador
                            org.Usuario = tmpUser;
                            db.Organizadores.Add(org);
                            //Guardo el Organizador en la base
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
            //El destino de la redireccion depende del resultado de la operacion
            return RedirectToAction(accion, parametroDeAccion);
        }

        [HttpGet]
        public ActionResult Editar()
        {
            if (!this.esOrganizador())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {

                Organizador org = null;
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    org = db.Organizadores.Find(Session["OrganizadorLogueado"].ToString());
                }
                return View(org);
            }
        }
        [HttpPost]
        public ActionResult Editar(Organizador org)
        {
            if (!this.esOrganizador())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {
                //Defino variales auxiliares para el retorno
                var parametroDeAccion = (Object)null;
                string accion = string.Empty;
                if (org != null)
                {
                    using (GestionEventosContext db = new GestionEventosContext())
                    {
                        Organizador auxOrg = db.Organizadores.Find(Session["OrganizadorLogueado"].ToString());
                        if (auxOrg != null)
                        {
                            //asigno el nuevo telefono al Organizador
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

            if (!this.esAdmin())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {
                var lista = new List<Organizador>();
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    lista = db.Organizadores.ToList();
                }
                return View(lista);
            }
        }

        public bool esAdmin()
        {
            return Session["TipoDeUsuario"] != null && Session["TipoDeUsuario"].ToString() == "Administrador" ? true : false;
        }
        public bool esOrganizador()
        {
            return Session["TipoDeUsuario"] != null && Session["TipoDeUsuario"].ToString() == "Organizador" ? true : false;
        }
    }
}
