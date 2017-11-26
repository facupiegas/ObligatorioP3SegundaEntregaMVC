using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("Organizadores")]
    public class Organizador
    {
        #region Atributos y Properties
        [Key]
        [Display(Name ="Nombre Organizador")]
        [Required(ErrorMessage ="Debe ingresar un Nombre")]
        public string NombreOrganizador { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Telefono")]
        public string Telefono { get; set; }
        public virtual Usuario Usuario { get; set; }
        #endregion

        #region Metodos

        public Organizador(string unNombre, string unEmail, string unTelefono, Usuario unUsuario)
        {
            this.NombreOrganizador = unNombre;
            this.Email = unEmail;
            this.Telefono = unTelefono;
            this.Usuario = unUsuario;
        }
        public Organizador() { }
        #endregion
    }
}
