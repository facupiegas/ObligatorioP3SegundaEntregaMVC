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

        //esta accion solo puede ser utilizada por el organizador logueado 
        public ActionResult CalificarProveedor(string nombreEvento = "")
        {
            CalificarProveedorViewModel vm = new CalificarProveedorViewModel();
            using (GestionEventosContext db = new GestionEventosContext())
            {
                vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Realizado).ToList();
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
        [HttpGet]
        public ActionResult CrearCalificacion(string rut,string nombreEvento,DateTime fecha,string nombreServicio)
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
                NombreEvento=nombreEvento,
                NombreServicio = nombreServicio,
                Fecha = fecha
            };
            CrearCalificacionViewModel vm = new CrearCalificacionViewModel();
            vm.CalificacionProveedor = calificacionProveedor;
            vm.ServicioContratado = servicioContratado;
            Session["CrearCalificacion"] = vm;
            return View(vm);
        }
        [HttpPost]
        public ActionResult CrearCalificacion(CrearCalificacionViewModel vm)
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
                        ServicioContratado auxServContratado = db.ServicioContratados.Find(vm.ServicioContratado.Fecha,tmpProv.Rut,vm.ServicioContratado.NombreServicio,vm.CalificacionProveedor.NombreEvento);
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

            return RedirectToAction(accion,parametroDeAccion);
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