using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("ServicioContratados")]
    public class ServicioContratado
    {

        #region Atributos y Properties
        [Key]
        [Column(Order =1)]
        public DateTime Fecha { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Rut { get; set; }
        [Key]
        [Column(Order = 3)]
        public string NombreServicio { get; set; }
        [Key]
        [Column(Order = 4)]
        public string NombreEvento { get; set; }
        public virtual Servicio Servicio { get; set; }
        public bool yaCalificado { get; set; }

        #endregion

        #region Metodos
        public ServicioContratado(Proveedor unProveedor, List<Servicio> unaListaDeServicios)
        {
            //this.Proveedor = unProveedor;
            //this.ListaServicios = unaListaDeServicios;
        }
        public ServicioContratado()
        {
            //this.ListaServicios = new List<Servicio>();
        }
        #endregion
    }
}
