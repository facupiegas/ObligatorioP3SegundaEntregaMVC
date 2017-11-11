using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3MVC.Models;
using ObligatorioP3MVC.ViewModels;

namespace ObligatorioP3MVC.Controllers
{
    public class EventoController : Controller
    {
        public ActionResult Error(string mensaje)
        {
            if (mensaje == null || mensaje == "") mensaje = "No hay detalle para el Error";
            ViewBag.Error = mensaje;
            return View();
        }
        public ActionResult Exito(string mensaje)
        {
            if (mensaje == null || mensaje == "") mensaje = "No hay detalle para mostrar";
            ViewBag.Mensaje = mensaje;
            return View();
        }
        public ActionResult EventosEntreFechas(DateTime? fechaInicial =null,DateTime? fechaFinal=null)
        {
            if (!this.esAdmin())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {
                //Si la fecha inicial es mayor a la final devuelvo un error
                if (fechaInicial > fechaFinal)
                {
                    ModelState.AddModelError("", "La fecha incial debe ser menor a la fecha final.");
                }

                EventoServicioContratadoViewModel vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    if (ModelState.IsValid)
                    {
                        bool evaluarOtrosCasos = true;
                        if (fechaInicial != null & fechaFinal != null)//si las dos fechas fueron ingresadas
                        {
                            evaluarOtrosCasos = false; //no evaluo los demas casos
                            vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Fecha >= fechaInicial).Where(p => p.Fecha <= fechaFinal).ToList();
                        }
                        if (evaluarOtrosCasos)//compruebo si fue ingresada alguna de las fechas o ninguna
                        {
                            if (fechaInicial != null)
                            {
                                vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Fecha >= fechaInicial).ToList();
                            }

                            if (fechaFinal != null)
                            {
                                vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Fecha <= fechaFinal).ToList();
                            }
                            if (fechaFinal == null && fechaInicial == null)
                            {
                                vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").ToList();
                            }
                        }
                    }

                }
                return View(vm);
            }
        }
          

        public ActionResult ListadoMejorCalificados()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            GestionEventosContext db = new GestionEventosContext();
            listaProveedores = db.Proveedores.GroupBy(p => p.CalificacionGeneral)
                      .OrderByDescending(grupo => grupo.Key)
                      .First().ToList();

            return View(listaProveedores);
        }

        public ActionResult ListadoMasElegidos()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            GestionEventosContext db = new GestionEventosContext();
            listaProveedores = db.Proveedores.GroupBy(p => p.CantVecesElegido)
                      .OrderByDescending(grupo => grupo.Key)
                      .First().ToList();
            return View(listaProveedores);
        }

        public ActionResult ListarEventosDeOrganizador(string nombreEvento = "",string nombreOrganizador="")
        {
            if (!this.esOrganizador() && !this.esAdmin())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }

            EventoServicioContratadoViewModel vm = null;
            if (this.esOrganizador())//si el rol del usuario autenticado es Organizador
            {
                vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {

                    string org = Session["OrganizadorLogueado"].ToString();
                    //cargo los eventos asociados al Organizador autenticado
                    vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Organizador.NombreOrganizador == org).ToList();
                    if (nombreEvento != "")
                    {//cuando un evento es seleccionado en la vista se pasa el nombre del evento por parametro para cargar sus ServicioContratado
                        ViewBag.NombreEvento = nombreEvento;
                        vm.ServiciosContratados = db.ServicioContratados.Where(p => p.NombreEvento == nombreEvento).ToList();
                    }
                    else
                    {
                        ViewBag.NombreEvento = null;
                    }
                }           
            }
            if (this.esAdmin())//si el rol del usuario autenticado es Administrador
            {
                vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    //cargo la lista de Organizadores
                    vm.Organizadores = db.Organizadores.ToList();
                    if (nombreOrganizador != "")
                    {//al seleccionar un Organizador en la vista se pasa su nombre por parametro para cargar los eventos asociados al Organizador
                        ViewBag.NombreOrganizador = nombreOrganizador;
                        vm.Eventos = db.Eventos.Where(p => p.Organizador.NombreOrganizador == nombreOrganizador).ToList();
                    }
                    else
                    {
                        ViewBag.NombreOrganizador = null;
                    }
                    if (nombreEvento != "")
                    {//cuando un evento es seleccionado en la vista se pasa el nombre del evento por parametro para cargar sus ServicioContratado
                        ViewBag.NombreEvento = nombreEvento;
                        vm.ServiciosContratados = db.ServicioContratados.Where(p => p.NombreEvento == nombreEvento).ToList();
                    }
                    else
                    {
                        ViewBag.NombreEvento = null;
                    }
                }
            }

            return View(vm);
        }

        public ActionResult CalificarProveedor(string nombreEvento = "")
        {
            if (!this.esOrganizador())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {
                EventoServicioContratadoViewModel vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    string org = Session["OrganizadorLogueado"].ToString();
                    //cargo los eventos realizados asociados al Organizador autenticado
                    vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Realizado).Where(p => p.Organizador.NombreOrganizador == org ).ToList();
                    if (nombreEvento != "")
                    {//cuando un evento es seleccionado en la vista se pasa el nombre del evento por parametro para cargar sus ServicioContratado
                        ViewBag.NombreEvento = nombreEvento;
                        vm.ServiciosContratados = db.ServicioContratados.Where(p => p.NombreEvento == nombreEvento).ToList();
                    }
                    else
                    {
                        ViewBag.NombreEvento = null;
                    }
                }
                return View(vm);
            }
        }
        [HttpGet]
        public ActionResult CrearCalificacion(string rut,string nombreEvento,DateTime fecha,string nombreServicio)
        {
            if (!this.esOrganizador())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {

                ViewBag.Rut = rut;
                ViewBag.NombreServicio = nombreServicio;
                ViewBag.NombreEvento = nombreEvento;
                //con los datos ingresados por parametro, creo los objetos necesarios para dar de alta una calificacion 
                CalificacionProveedor calificacionProveedor = new CalificacionProveedor()
                {
                    Rut = rut,
                    NombreEvento = nombreEvento
                };
                ServicioContratado servicioContratado = new ServicioContratado()
                {
                    Rut = rut,
                    NombreEvento = nombreEvento,
                    NombreServicio = nombreServicio,
                    Fecha = fecha
                };
                CrearCalificacionViewModel vm = new CrearCalificacionViewModel();
                vm.CalificacionProveedor = calificacionProveedor;
                vm.ServicioContratado = servicioContratado; 
                Session["CrearCalificacion"] = vm;
                return View(vm);
            }
        }
        [HttpPost]
        public ActionResult CrearCalificacion(CrearCalificacionViewModel vm)
        {
            if (!this.esOrganizador())
            {
                return RedirectToAction("Logout", "Home", new { mensaje = @"Usted no tiene los permisos necesarios 
                                                            para utilizar el recurso.
                                                            Por favor inicie sesión con las credenciales adecuadas" });
            }
            else
            {
                var parametroDeAccion = (Object)null;
                string accion = string.Empty;
                //Recupero el objeto creado anteriormente con los datos ingresados por parametro
                CrearCalificacionViewModel aux = (CrearCalificacionViewModel)Session["CrearCalificacion"];
                //cargo la calificacion y el comentario en el objeto
                aux.CalificacionProveedor.Calificacion = vm.CalificacionProveedor.Calificacion;
                aux.CalificacionProveedor.Comentario = vm.CalificacionProveedor.Comentario;
                vm = aux;
                if (ModelState.IsValid)
                {
                    using (GestionEventosContext db = new GestionEventosContext())
                    {
                        //busco al proveedor para agregarle el comentario
                        Proveedor tmpProv = db.Proveedores.Find(vm.CalificacionProveedor.Rut);
                        if (tmpProv != null)
                        {
                            if (vm.CalificacionProveedor.Comentario == null || vm.CalificacionProveedor.Comentario == "")
                            {
                                vm.CalificacionProveedor.Comentario = "No fue ingresado un comentario adicional";
                            }
                            tmpProv.Calificaciones.Add(vm.CalificacionProveedor);
                            //busco el ServicioContratado con los datos del evento y proveedor
                            ServicioContratado auxServContratado = db.ServicioContratados.Find(vm.ServicioContratado.Fecha, tmpProv.Rut, vm.ServicioContratado.NombreServicio, vm.CalificacionProveedor.NombreEvento);
                            //indico que ya fue calificado para no tomarlo en cuenta en futuras calificaciones
                            auxServContratado.yaCalificado = true;
                            try
                            {
                                db.SaveChanges();
                                accion = "Exito";
                                parametroDeAccion = new { mensaje = "Su comentario fue ingresado con exito!. Muchas gracias" };
                            }
                            catch
                            {
                                accion = "Error";
                                parametroDeAccion = new { mensaje = "Su comentario no pudo ser ingresado. Por favor verifique los datos ingresados e intentelo nuevamente." };
                            }

                        }
                        else
                        {
                            accion = "Error";
                            parametroDeAccion = new { mensaje = "Por favor verifique los datos ingresados e intentelo nuevamente." };
                        }
                    }
                }
                else
                {
                    accion = "Error";
                    parametroDeAccion = new { mensaje = "Por favor verifique los datos ingresados e intentelo nuevamente." };
                }
                //segun el resultado de las operaciones puede variar a que accion se redirecciona
                return RedirectToAction(accion, parametroDeAccion);
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