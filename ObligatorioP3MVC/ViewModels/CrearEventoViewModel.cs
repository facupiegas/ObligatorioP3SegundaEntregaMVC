using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace ObligatorioP3MVC.ViewModels
{
    public class CrearEventoViewModel
    {
        public Evento Evento { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        [Display(Name ="Tipo Evento")]
        public SelectList TipoEventos { get; set; }

        public string IdTipoEvento { get; set; }
        [Display(Name = "Tipo Servicio")]
        public SelectList TipoServicios { get; set; }

        public string IdTipoServicio { get; set; }

        public CrearEventoViewModel(List<TipoEvento> tipoEventos)
        {
            this.TipoEventos = new SelectList(tipoEventos, "NombreTipoEvento", "NombreTipoEvento");
            //this.TipoServicios = new SelectList(TipoServicios, "NombreTipoServicio", "NombreTipoServicio");
        }
        public CrearEventoViewModel() { }
    }
}