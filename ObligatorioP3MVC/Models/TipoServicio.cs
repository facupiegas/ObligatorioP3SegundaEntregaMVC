using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("TipoServicios")]
    public class TipoServicio
    {
        #region Atributos y properties
        [Key]
        public string NombreTipoServicio { get; set; }
        public virtual List<TipoEvento> TiposEventos { get; set; }
        #endregion

        #region Constructores
        public TipoServicio(string unNombre, List<TipoEvento> tiposEventos)
        {
            this.NombreTipoServicio = unNombre;
            this.TiposEventos = tiposEventos;

        }
        public TipoServicio()
        {
            this.TiposEventos = new List<TipoEvento>();
        }
        #endregion

    }
}
