using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("TipoEventos")]
    public class TipoEvento
    {
        #region Atributos y Properties
        [Key]
        [Display(Name ="Tipo Evento")]
        public string NombreTipoEvento { get; set; }

        public virtual List<TipoServicio> TiposServicios { get; set; }

        
        #endregion

        #region Constructores
        public TipoEvento(string unNombre, List<TipoServicio> unaLista)
        {
            this.NombreTipoEvento = unNombre;
            this.TiposServicios = unaLista;
            
        }

        public TipoEvento()
        {
            this.TiposServicios = new List<TipoServicio>();
        }
        #endregion
    }
}
