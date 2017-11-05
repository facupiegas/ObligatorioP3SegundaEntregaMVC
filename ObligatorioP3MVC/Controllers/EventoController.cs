using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult CalificarProveedor()
        {
            return View();
        }
    }
}