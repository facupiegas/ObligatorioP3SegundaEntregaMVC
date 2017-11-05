using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligatorioP3MVC.Models;

namespace ObligatorioP3MVC.ViewModels
{
    public class CrearCalificacionViewModel
    {
        public CalificacionProveedor CalificacionProveedor { get; set; }
        public ServicioContratado ServicioContratado { get; set; }
    }
}