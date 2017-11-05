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

        [HttpGet]
        //esta accion solo puede ser utilizada por el organizador logueado 
        public ActionResult CalificarProveedor(string nombreEvento)
        {

            CalificarProveedorViewModel vm = new CalificarProveedorViewModel(); 
            
            using (GestionEventosContext db = new GestionEventosContext())
            {
                vm.Eventos = db.Eventos.Include("Organizador").Include("TipoEvento").Where(p => p.Realizado).ToList();
                if (nombreEvento != null || nombreEvento != "")
                {
                    vm.ServiciosContratados = db.ServicioContratados.Where(p => p.NombreEvento == nombreEvento).ToList();
                }
            }
            return View(vm);
        }
        

        public static bool CalificarProveedor(Evento unEvento, string unRut, int unPuntaje, string unComentario)
        {
            bool retorno = false;
            //compruebo que el valor del puntaje este dentro del rango
            if (unPuntaje > 0 && unPuntaje <= 5) //////ESTA LINEA PROBABLEMENTE SE SUPLANTA POR UNA ANNOTATION EN LA CLASE
            {
                using (var db = new GestionEventosContext())
                {
                    //busco el evento en la bd
                    unEvento = db.Eventos.Find(unEvento.Nombre);
                    //si encuentro el evento, la fecha es menor a la actual y el evento fue realizado
                    if (unEvento != null && unEvento.Fecha < DateTime.Now && unEvento.Realizado)
                    {
                        //busco el Proveedor con el rut ingresado
                        Proveedor tmpProv = db.Proveedores.Find(unRut);
                        if (tmpProv != null)
                        {
                            bool okParaCalificar = true;
                            //verifico que el proveedor no haya sido previamente calificado para este evento
                            foreach (ServicioContratado tmpContratado in unEvento.ServiciosContratados)
                            {
                                if (tmpContratado.Rut == unRut && tmpContratado.yaCalificado)
                                {
                                    okParaCalificar = false;
                                }
                            }
                            if (okParaCalificar)
                            {
                                //Si no fue ingresado ningun comenario lo seteo en null
                                if (unComentario == "")
                                {
                                    unComentario = null;
                                }
                                CalificacionProveedor calificacion = new CalificacionProveedor() { Rut = unRut, NombreEvento = unEvento.Nombre, Calificacion = unPuntaje, Comentario = unComentario };
                                tmpProv.Calificaciones.Add(calificacion);
                                //Marco al proveedor como ya calificado para este evento
                                foreach (ServicioContratado tmpContratado in unEvento.ServiciosContratados)
                                {
                                    if (tmpContratado.Rut == unRut)
                                    {
                                        tmpContratado.yaCalificado = true;
                                    }
                                }
                                try
                                {
                                    db.SaveChanges();
                                    retorno = true;

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            return retorno;
        }

    }
}