using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        #region Atributos y Properties

        public enum EnumRol { Administrador, Proveedor, Organizador }
        [Key]
        public string Nombre { set; get; }
        [Display(Name ="Password")]
        [Required(ErrorMessage ="Debe ingresar un Password")]
        public string Pass { set; get; }
        public EnumRol Rol { set; get; }
        public string Sal { get; set; }
        #endregion

        #region Constructores
        public Usuario(string unNombre, string unaPass, EnumRol unRol)
        {
            this.Sal = GenerarSal();
            string hash = GenerarSHA256Hash(unaPass, this.Sal);
            this.Nombre = unNombre;
            this.Pass = hash;
            this.Rol = unRol;
        }
        public Usuario() { }
        #endregion

        #region Otros metodos
        public static String GenerarSal()
        {
            var random = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[6];
            random.GetBytes(bytes);
            return Convert.ToBase64String(bytes);

        }
        public static String GenerarSHA256Hash(String pass, String sal)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass + sal);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public EnumRol ObtenerTipo()
        {
            return this.Rol;
        }


        #endregion
    }
}
