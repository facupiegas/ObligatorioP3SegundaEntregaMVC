using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("Eventos")]
    public class Evento
    {
        #region Atributos y Properties
        [Key]
        [Column(Order =1)]
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public virtual Organizador Organizador { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public virtual List<ServicioContratado> ServiciosContratados { get; set; }
        public bool Realizado { get; set; }
        #endregion

        #region Constructores
        public Evento(String unNombre, DateTime unaFecha, TipoEvento unTipoEvento, string unaDireccion,bool realizado,Organizador unOrganizador)
        {
            this.ServiciosContratados = new List<ServicioContratado>();
            this.Nombre = unNombre;
            this.Fecha = unaFecha;
            this.TipoEvento = unTipoEvento;
            this.Direccion = unaDireccion;
            this.Realizado = realizado;
            this.Organizador = unOrganizador;
        }
        public Evento()
        {
            this.ServiciosContratados = new List<ServicioContratado>();
        }


        #endregion
    }
}
