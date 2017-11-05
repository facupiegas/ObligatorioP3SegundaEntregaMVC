using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("CalificacionProveedores")]
    public class CalificacionProveedor
    {
        [Key]
        [Column(Order = 1)]
        public string Rut { get; set; }
        [Key]
        [Column(Order = 2)]
        public string NombreEvento { get; set; }
        [Required(ErrorMessage ="Debe ingresar una calificacion(entre 1 y 5)")]
        [Range(1,5)]
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        

        public CalificacionProveedor()
        {}
    }
}
