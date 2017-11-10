using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligatorioP3MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ObligatorioP3MVC.ViewModels
{
    public class EventoServicioContratadoViewModel
    {
        public IEnumerable<Evento> Eventos { get; set; }
        public IEnumerable<ServicioContratado> ServiciosContratados { get; set; }
        public IEnumerable<Organizador> Organizadores { get; set; }
        [Display(Name="Fecha Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicial { get; set; }
        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinal { get; set; }
    }
}