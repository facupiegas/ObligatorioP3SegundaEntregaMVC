using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace ObligatorioP3MVC.Models
{
    public class GestionEventosContext:DbContext
    {
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<TipoEvento> TipoEventos { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<CalificacionProveedor> CalificacionProveedores { get; set; }
        public DbSet<ServicioContratado> ServicioContratados { get; set; }

        public GestionEventosContext():base(@"Server =.\; DataBase = ObligatorioP3MVC; User Id = sa; Password = Admin1234!; MultipleActiveResultSets = true;"){ }

        public static void PoblarDB()
        {
            GestionEventosContext db = new GestionEventosContext();
            //CARGO TIPOEVENTOS Y TIPOSERVICIOS
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "/tipoServicioTipoEvento.txt");
            }
            catch (Exception)
            {
                Console.WriteLine("NO SE PUDO LEER EL TXT!");
            }
            if (sr != null)
            {
                bool quedanLineas = true;
                while (quedanLineas)
                {
                    string linea = sr.ReadLine();
                    if (linea == null)
                    {
                        quedanLineas = false;
                    }
                    else
                    {
                        string[] arr = linea.Split('#'); //Separo los TipoServicio de sus TipoEventos
                        if (db.TipoServicios.Find(arr[0]) == null)
                        {
                            //creo el TipoServicio
                            TipoServicio auxTipoServicio = new TipoServicio() { NombreTipoServicio = arr[0] };

                            if (arr.Count() > 1)//Si el array generado tiene mas de un elemento quiere decir que tiene TipoEvento asociados
                            {
                                //Separo los distintos TipoEvento para el TipoServicio creado
                                string[] arr2 = arr[1].Split(':');
                                for (int i = 1; i < arr2.Count(); i++)
                                {
                                    //compruebo si el TipoEvento ya existe en la base de datos
                                    TipoEvento existe = db.TipoEventos.Find(arr2[i]);
                                    if (existe == null)
                                    {
                                        //si no existe lo creo 
                                        TipoEvento auxTipoEvento = new TipoEvento() { NombreTipoEvento = arr2[i] };
                                        //agrego a la lista de TipoEvento del objeto TipoServicio
                                        auxTipoServicio.TiposEventos.Add(auxTipoEvento);

                                    }
                                    else
                                    {
                                        //agrego a la lista de TipoEvento del objeto TipoServicio
                                        auxTipoServicio.TiposEventos.Add(existe);
                                    }
                                }

                            }
                            //agrego el TipoServicio al Context
                            db.TipoServicios.Add(auxTipoServicio);
                        }

                    }
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Carga por txt. Los datos ya fueron agregados a la bd");
            }
            try
            {
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //CARGO PROVEEDORES Y SUS SERVICIOS
            GestionEventosContext db2 = new GestionEventosContext();
            sr = null;
            try
            {
                sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "proveedorServicio.txt");
            }
            catch (Exception)
            {
                Console.WriteLine("NO SE PUDO LEER EL TXT!");
            }
            if (sr != null)
            {
                bool quedanLineas = true;
                while (quedanLineas)
                {
                    string linea = sr.ReadLine();
                    if (linea == null)
                    {
                        quedanLineas = false;
                    }
                    else
                    {
                        //separo el Proveedor de sus Servicios
                        string[] arr = linea.Split('|');
                        //separo los datos del Proveedor
                        string[] datosProvArr = arr[0].Split('#');
                        //creo el Proveedor con los datos
                        bool vip = datosProvArr[4].TrimEnd() == "Vip" ? true : false;
                        Proveedor tmpProveedor = new Proveedor()
                        {
                            Rut = datosProvArr[0],
                            NomFantasia = datosProvArr[1],
                            Email = datosProvArr[2],
                            Telefono = datosProvArr[3],
                            Fecha = DateTime.Now,
                            Vip = vip

                        };
                        //agrego servicios al proveedor
                        //separo cada Servicio del Proveedor
                        string[] servProvArr = arr[1].Split('#');
                        for (int i = 0; i < servProvArr.Count(); i++)
                        {
                            //separo los datos de cada Servicio
                            string[] datosServArr = servProvArr[i].Split(':');
                            //busco si el TipoServicio del Servicio ya existe en la base de datos
                            TipoServicio existe = db2.TipoServicios.Find(datosServArr[3]);
                            if (existe == null)
                            {
                                //si no existe lo creo
                                existe = new TipoServicio() { NombreTipoServicio = datosServArr[3] };
                            }
                            //creo el Servicio con los datos
                            Servicio tmpServicio = new Servicio()
                            {
                                Rut = datosProvArr[0],
                                NombreServicio = datosServArr[0].TrimStart(),
                                Descripcion = datosServArr[1],
                                Imagen = datosServArr[2],
                                TipoServicio = existe
                            };
                            //recorro la lista de Servicios del Proveedor
                            //para verificar si algun Servicio ya contiene el TipoServicio actual
                            //y asi evitar registros duplicados de TipoServicio en la base de datos
                            foreach (Servicio x in tmpProveedor.ServiciosOfrecidos)
                            {
                                if (x.TipoServicio.NombreTipoServicio == existe.NombreTipoServicio)
                                {
                                    tmpServicio.TipoServicio = db2.TipoServicios.Find(existe.NombreTipoServicio);
                                }
                            }
                            //agrego el Servicio al Proveedor
                            tmpProveedor.ServiciosOfrecidos.Add(tmpServicio);
                        }
                        //agrego el Proveedor al Context
                        db2.Proveedores.Add(tmpProveedor);
                    }

                }
            }
            try
            {
                db2.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }//fin PoblarDB

    }

}
