using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("Servicios")]
    public class Servicio
    {
        #region Atributos y Proeperties
        [Key]
        [Column(Order =1)]
        public string Rut { get; set; }
        [Key]
        [Column(Order =2)]
        public string NombreServicio { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public virtual TipoServicio TipoServicio { get; set; }
        #endregion

        #region Constructores
        public Servicio(string unNombre, string unaDescripcion, TipoServicio unTipoServicio)
        {
            this.NombreServicio = unNombre;
            this.Descripcion = unaDescripcion;
            this.Imagen = null;
            this.TipoServicio = unTipoServicio;
        }
        public Servicio() { }
        #endregion
    }
}
