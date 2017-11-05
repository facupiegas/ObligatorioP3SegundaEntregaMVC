using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3MVC.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {
        #region Atributos y Properties
        
        [Key]
        public string Rut { get; set; }

        public string NomFantasia { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public DateTime Fecha { get; set; }

        public bool Vip { get; set; }

        public virtual List<Servicio> ServiciosOfrecidos { get; set; }
        public virtual List<CalificacionProveedor> Calificaciones { get; set; }

        private double calificacionGeneral; 
        public double CalificacionGeneral
        {
            get { return CalcularPromedioCalificacion(); }
            set { calificacionGeneral = value; }
        }

        private int cantVecesElegido;
        public int CantVecesElegido
        {
            get { return Calificaciones.Count; }
            set { cantVecesElegido = value; }
        }

        #endregion

        #region Constructores
        public Proveedor(string unRut, string unNomFantasia, string unEmail, string unTelefono, DateTime unaFecha, bool esVip)
        {
            this.Rut = unRut;
            this.NomFantasia = unNomFantasia;
            this.Email = unEmail;
            this.Telefono = unTelefono;
            this.Fecha = unaFecha;
            this.Vip = esVip;
            this.ServiciosOfrecidos = new List<Servicio>();
            this.Calificaciones = new List<CalificacionProveedor>();
            this.CantVecesElegido = Calificaciones.Count;
            

        }

        public Proveedor()
        {
            this.ServiciosOfrecidos = new List<Servicio>();
            this.Calificaciones = new List<CalificacionProveedor>();
            this.CantVecesElegido = Calificaciones.Count;
        }

        #endregion

        public double CalcularPromedioCalificacion()
        { //Metodo para calcular la Calificacion de un Proveedor
            double promedio = 0;
            int suma = 0;
            if (Calificaciones.Count > 0)
            {
                foreach (CalificacionProveedor tmpCalificacion in this.Calificaciones)
                {                   
                    suma += tmpCalificacion.Calificacion;                    
                }
                promedio = suma / Calificaciones.Count;
            }
            return promedio;
        }
    }
}
