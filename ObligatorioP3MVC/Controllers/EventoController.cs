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
                EventoServicioContratadoViewModel vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {

                    ////HAY QUE CHECKEAR QUE LOS RANGOS DE FECHAS ESTEN BIEN, FINCIAL MENOR QUE FMAYOR BLA BLA BLA
                    fechaInicial = new DateTime(2015, 12, 10); ////////////////////dato de prueba... SACAR!!!
                    fechaFinal = new DateTime(2015, 12, 11); ////////////////////dato de prueba... SACAR!!!
                    bool evaluarOtrosCasos = true;
                    if (fechaInicial != null & fechaFinal != null)
                    {
                        evaluarOtrosCasos = false;
                        vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Fecha >= fechaInicial).Where(p => p.Fecha <= fechaFinal).ToList();
                    }
                    if (evaluarOtrosCasos)
                    {
                        fechaInicial = new DateTime(2015, 12, 11); ////////////////////dato de prueba... SACAR!!!
                        if (fechaInicial != null)
                        {
                            vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Fecha >= fechaInicial).ToList();
                        }
                        fechaFinal = new DateTime(2016, 12, 11); ////////////////////dato de prueba... SACAR!!!
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

            if (this.esOrganizador())
            {
                vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {

                    string org = Session["OrganizadorLogueado"].ToString();
                    vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Organizador.NombreOrganizador == org).ToList();
                    if (nombreEvento != "")
                    {
                        ViewBag.NombreEvento = nombreEvento;
                        vm.ServiciosContratados = db.ServicioContratados.Where(p => p.NombreEvento == nombreEvento).ToList();
                    }
                    else
                    {
                        ViewBag.NombreEvento = null;
                    }
                }           
            }
            if (this.esAdmin())
            {
                vm = new EventoServicioContratadoViewModel();
                using (GestionEventosContext db = new GestionEventosContext())
                {
                    vm.Organizadores = db.Organizadores.ToList();
                    if (nombreOrganizador != "")
                    {
                        ViewBag.NombreOrganizador = nombreOrganizador;
                        vm.Eventos = db.Eventos.Where(p => p.Organizador.NombreOrganizador == nombreOrganizador).ToList();
                    }
                    else
                    {
                        ViewBag.NombreOrganizador = null;
                    }
                    if (nombreEvento != "")
                    {
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
                    vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Realizado).Where(p => p.Organizador.NombreOrganizador == org ).ToList();
                    if (nombreEvento != "")
                    {
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
                CrearCalificacionViewModel aux = (CrearCalificacionViewModel)Session["CrearCalificacion"];
                aux.CalificacionProveedor.Calificacion = vm.CalificacionProveedor.Calificacion;
                aux.CalificacionProveedor.Comentario = vm.CalificacionProveedor.Comentario;
                vm = aux;
                if (ModelState.IsValid)
                {
                    using (GestionEventosContext db = new GestionEventosContext())
                    {
                        Proveedor tmpProv = db.Proveedores.Find(vm.CalificacionProveedor.Rut);
                        if (tmpProv != null)
                        {
                            if (vm.CalificacionProveedor.Comentario == null || vm.CalificacionProveedor.Comentario == "")
                            {
                                vm.CalificacionProveedor.Comentario = "No fue ingresado ningun comentario";
                            }
                            tmpProv.Calificaciones.Add(vm.CalificacionProveedor);
                            ServicioContratado auxServContratado = db.ServicioContratados.Find(vm.ServicioContratado.Fecha, tmpProv.Rut, vm.ServicioContratado.NombreServicio, vm.CalificacionProveedor.NombreEvento);
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

                return RedirectToAction(accion, parametroDeAccion);
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