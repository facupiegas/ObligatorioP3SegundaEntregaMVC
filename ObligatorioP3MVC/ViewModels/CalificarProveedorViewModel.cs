﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligatorioP3MVC.Models;

namespace ObligatorioP3MVC.ViewModels
{
    public class CalificarProveedorViewModel
    {
        public IEnumerable<Evento> Eventos { get; set; }
        public IEnumerable<ServicioContratado> ServiciosContratados { get; set; }
    }
}