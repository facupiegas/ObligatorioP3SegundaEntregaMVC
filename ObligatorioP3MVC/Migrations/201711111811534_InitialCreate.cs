namespace ObligatorioP3MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalificacionProveedores",
                c => new
                    {
                        Rut = c.String(nullable: false, maxLength: 128),
                        NombreEvento = c.String(nullable: false, maxLength: 128),
                        Calificacion = c.Int(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => new { t.Rut, t.NombreEvento })
                .ForeignKey("dbo.Proveedores", t => t.Rut, cascadeDelete: true)
                .Index(t => t.Rut);
            
            CreateTable(
                "dbo.Eventos",
                c => new
                    {
                        Nombre = c.String(nullable: false, maxLength: 128),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                        Direccion = c.String(nullable: false),
                        Realizado = c.Boolean(nullable: false),
                        Organizador_NombreOrganizador = c.String(maxLength: 128),
                        TipoEvento_NombreTipoEvento = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Nombre)
                .ForeignKey("dbo.Organizadores", t => t.Organizador_NombreOrganizador)
                .ForeignKey("dbo.TipoEventos", t => t.TipoEvento_NombreTipoEvento)
                .Index(t => t.Organizador_NombreOrganizador)
                .Index(t => t.TipoEvento_NombreTipoEvento);
            
            CreateTable(
                "dbo.Organizadores",
                c => new
                    {
                        NombreOrganizador = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false),
                        Telefono = c.String(),
                        Usuario_Nombre = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NombreOrganizador)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Nombre)
                .Index(t => t.Usuario_Nombre);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Nombre = c.String(nullable: false, maxLength: 128),
                        Pass = c.String(),
                        Rol = c.Int(nullable: false),
                        Sal = c.String(),
                    })
                .PrimaryKey(t => t.Nombre);
            
            CreateTable(
                "dbo.ServicioContratados",
                c => new
                    {
                        Fecha = c.DateTime(nullable: false),
                        Rut = c.String(nullable: false, maxLength: 128),
                        NombreServicio = c.String(nullable: false, maxLength: 128),
                        NombreEvento = c.String(nullable: false, maxLength: 128),
                        yaCalificado = c.Boolean(nullable: false),
                        Evento_Nombre = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Fecha, t.Rut, t.NombreServicio, t.NombreEvento })
                .ForeignKey("dbo.Servicios", t => new { t.Rut, t.NombreServicio }, cascadeDelete: true)
                .ForeignKey("dbo.Eventos", t => t.Evento_Nombre)
                .Index(t => new { t.Rut, t.NombreServicio })
                .Index(t => t.Evento_Nombre);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        Rut = c.String(nullable: false, maxLength: 128),
                        NombreServicio = c.String(nullable: false, maxLength: 128),
                        Imagen = c.String(),
                        Descripcion = c.String(),
                        TipoServicio_NombreTipoServicio = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Rut, t.NombreServicio })
                .ForeignKey("dbo.TipoServicios", t => t.TipoServicio_NombreTipoServicio)
                .ForeignKey("dbo.Proveedores", t => t.Rut, cascadeDelete: true)
                .Index(t => t.Rut)
                .Index(t => t.TipoServicio_NombreTipoServicio);
            
            CreateTable(
                "dbo.TipoServicios",
                c => new
                    {
                        NombreTipoServicio = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NombreTipoServicio);
            
            CreateTable(
                "dbo.TipoEventos",
                c => new
                    {
                        NombreTipoEvento = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NombreTipoEvento);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Rut = c.String(nullable: false, maxLength: 128),
                        NomFantasia = c.String(),
                        Email = c.String(),
                        Telefono = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Vip = c.Boolean(nullable: false),
                        CalificacionGeneral = c.Double(nullable: false),
                        CantVecesElegido = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Rut);
            
            CreateTable(
                "dbo.TipoEventoTipoServicios",
                c => new
                    {
                        TipoEvento_NombreTipoEvento = c.String(nullable: false, maxLength: 128),
                        TipoServicio_NombreTipoServicio = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TipoEvento_NombreTipoEvento, t.TipoServicio_NombreTipoServicio })
                .ForeignKey("dbo.TipoEventos", t => t.TipoEvento_NombreTipoEvento, cascadeDelete: true)
                .ForeignKey("dbo.TipoServicios", t => t.TipoServicio_NombreTipoServicio, cascadeDelete: true)
                .Index(t => t.TipoEvento_NombreTipoEvento)
                .Index(t => t.TipoServicio_NombreTipoServicio);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servicios", "Rut", "dbo.Proveedores");
            DropForeignKey("dbo.CalificacionProveedores", "Rut", "dbo.Proveedores");
            DropForeignKey("dbo.Eventos", "TipoEvento_NombreTipoEvento", "dbo.TipoEventos");
            DropForeignKey("dbo.ServicioContratados", "Evento_Nombre", "dbo.Eventos");
            DropForeignKey("dbo.ServicioContratados", new[] { "Rut", "NombreServicio" }, "dbo.Servicios");
            DropForeignKey("dbo.Servicios", "TipoServicio_NombreTipoServicio", "dbo.TipoServicios");
            DropForeignKey("dbo.TipoEventoTipoServicios", "TipoServicio_NombreTipoServicio", "dbo.TipoServicios");
            DropForeignKey("dbo.TipoEventoTipoServicios", "TipoEvento_NombreTipoEvento", "dbo.TipoEventos");
            DropForeignKey("dbo.Eventos", "Organizador_NombreOrganizador", "dbo.Organizadores");
            DropForeignKey("dbo.Organizadores", "Usuario_Nombre", "dbo.Usuarios");
            DropIndex("dbo.TipoEventoTipoServicios", new[] { "TipoServicio_NombreTipoServicio" });
            DropIndex("dbo.TipoEventoTipoServicios", new[] { "TipoEvento_NombreTipoEvento" });
            DropIndex("dbo.Servicios", new[] { "TipoServicio_NombreTipoServicio" });
            DropIndex("dbo.Servicios", new[] { "Rut" });
            DropIndex("dbo.ServicioContratados", new[] { "Evento_Nombre" });
            DropIndex("dbo.ServicioContratados", new[] { "Rut", "NombreServicio" });
            DropIndex("dbo.Organizadores", new[] { "Usuario_Nombre" });
            DropIndex("dbo.Eventos", new[] { "TipoEvento_NombreTipoEvento" });
            DropIndex("dbo.Eventos", new[] { "Organizador_NombreOrganizador" });
            DropIndex("dbo.CalificacionProveedores", new[] { "Rut" });
            DropTable("dbo.TipoEventoTipoServicios");
            DropTable("dbo.Proveedores");
            DropTable("dbo.TipoEventos");
            DropTable("dbo.TipoServicios");
            DropTable("dbo.Servicios");
            DropTable("dbo.ServicioContratados");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Organizadores");
            DropTable("dbo.Eventos");
            DropTable("dbo.CalificacionProveedores");
        }
    }
}
