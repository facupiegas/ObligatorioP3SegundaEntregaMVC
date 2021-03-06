USE [master]
GO
/****** Object:  Database [ObligatorioP3MVC]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE DATABASE [ObligatorioP3MVC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ObligatorioP3MVC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ObligatorioP3MVC.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ObligatorioP3MVC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ObligatorioP3MVC_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ObligatorioP3MVC] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ObligatorioP3MVC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ObligatorioP3MVC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET ARITHABORT OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ObligatorioP3MVC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ObligatorioP3MVC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ObligatorioP3MVC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ObligatorioP3MVC] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ObligatorioP3MVC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET RECOVERY FULL 
GO
ALTER DATABASE [ObligatorioP3MVC] SET  MULTI_USER 
GO
ALTER DATABASE [ObligatorioP3MVC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ObligatorioP3MVC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ObligatorioP3MVC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ObligatorioP3MVC] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ObligatorioP3MVC] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ObligatorioP3MVC', N'ON'
GO
USE [ObligatorioP3MVC]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CalificacionProveedores]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalificacionProveedores](
	[Rut] [nvarchar](128) NOT NULL,
	[NombreEvento] [nvarchar](128) NOT NULL,
	[Calificacion] [int] NOT NULL,
	[Comentario] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CalificacionProveedores] PRIMARY KEY CLUSTERED 
(
	[Rut] ASC,
	[NombreEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Eventos]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eventos](
	[Nombre] [nvarchar](128) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Realizado] [bit] NOT NULL,
	[Organizador_NombreOrganizador] [nvarchar](128) NULL,
	[TipoEvento_NombreTipoEvento] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Eventos] PRIMARY KEY CLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organizadores]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizadores](
	[NombreOrganizador] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Usuario_Nombre] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Organizadores] PRIMARY KEY CLUSTERED 
(
	[NombreOrganizador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[Rut] [nvarchar](128) NOT NULL,
	[NomFantasia] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Telefono] [nvarchar](max) NULL,
	[Fecha] [datetime] NOT NULL,
	[Vip] [bit] NOT NULL,
	[CalificacionGeneral] [float] NOT NULL,
	[CantVecesElegido] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Proveedores] PRIMARY KEY CLUSTERED 
(
	[Rut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServicioContratados]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicioContratados](
	[Fecha] [datetime] NOT NULL,
	[Rut] [nvarchar](128) NOT NULL,
	[NombreServicio] [nvarchar](128) NOT NULL,
	[NombreEvento] [nvarchar](128) NOT NULL,
	[yaCalificado] [bit] NOT NULL,
	[Evento_Nombre] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.ServicioContratados] PRIMARY KEY CLUSTERED 
(
	[Fecha] ASC,
	[Rut] ASC,
	[NombreServicio] ASC,
	[NombreEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[Rut] [nvarchar](128) NOT NULL,
	[NombreServicio] [nvarchar](128) NOT NULL,
	[Imagen] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[TipoServicio_NombreTipoServicio] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Servicios] PRIMARY KEY CLUSTERED 
(
	[Rut] ASC,
	[NombreServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoEventos]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEventos](
	[NombreTipoEvento] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TipoEventos] PRIMARY KEY CLUSTERED 
(
	[NombreTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoEventoTipoServicios]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEventoTipoServicios](
	[TipoEvento_NombreTipoEvento] [nvarchar](128) NOT NULL,
	[TipoServicio_NombreTipoServicio] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TipoEventoTipoServicios] PRIMARY KEY CLUSTERED 
(
	[TipoEvento_NombreTipoEvento] ASC,
	[TipoServicio_NombreTipoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoServicios]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoServicios](
	[NombreTipoServicio] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TipoServicios] PRIMARY KEY CLUSTERED 
(
	[NombreTipoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/27/2017 09:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Nombre] [nvarchar](128) NOT NULL,
	[Pass] [nvarchar](max) NOT NULL,
	[Rol] [int] NOT NULL,
	[Sal] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Usuarios] PRIMARY KEY CLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Rut]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Rut] ON [dbo].[CalificacionProveedores]
(
	[Rut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Organizador_NombreOrganizador]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Organizador_NombreOrganizador] ON [dbo].[Eventos]
(
	[Organizador_NombreOrganizador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TipoEvento_NombreTipoEvento]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_TipoEvento_NombreTipoEvento] ON [dbo].[Eventos]
(
	[TipoEvento_NombreTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Usuario_Nombre]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Usuario_Nombre] ON [dbo].[Organizadores]
(
	[Usuario_Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Evento_Nombre]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Evento_Nombre] ON [dbo].[ServicioContratados]
(
	[Evento_Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Rut_NombreServicio]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Rut_NombreServicio] ON [dbo].[ServicioContratados]
(
	[Rut] ASC,
	[NombreServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Rut]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Rut] ON [dbo].[Servicios]
(
	[Rut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TipoServicio_NombreTipoServicio]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_TipoServicio_NombreTipoServicio] ON [dbo].[Servicios]
(
	[TipoServicio_NombreTipoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TipoEvento_NombreTipoEvento]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_TipoEvento_NombreTipoEvento] ON [dbo].[TipoEventoTipoServicios]
(
	[TipoEvento_NombreTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TipoServicio_NombreTipoServicio]    Script Date: 11/27/2017 09:01:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_TipoServicio_NombreTipoServicio] ON [dbo].[TipoEventoTipoServicios]
(
	[TipoServicio_NombreTipoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CalificacionProveedores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CalificacionProveedores_dbo.Proveedores_Rut] FOREIGN KEY([Rut])
REFERENCES [dbo].[Proveedores] ([Rut])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CalificacionProveedores] CHECK CONSTRAINT [FK_dbo.CalificacionProveedores_dbo.Proveedores_Rut]
GO
ALTER TABLE [dbo].[Eventos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Eventos_dbo.Organizadores_Organizador_NombreOrganizador] FOREIGN KEY([Organizador_NombreOrganizador])
REFERENCES [dbo].[Organizadores] ([NombreOrganizador])
GO
ALTER TABLE [dbo].[Eventos] CHECK CONSTRAINT [FK_dbo.Eventos_dbo.Organizadores_Organizador_NombreOrganizador]
GO
ALTER TABLE [dbo].[Eventos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Eventos_dbo.TipoEventos_TipoEvento_NombreTipoEvento] FOREIGN KEY([TipoEvento_NombreTipoEvento])
REFERENCES [dbo].[TipoEventos] ([NombreTipoEvento])
GO
ALTER TABLE [dbo].[Eventos] CHECK CONSTRAINT [FK_dbo.Eventos_dbo.TipoEventos_TipoEvento_NombreTipoEvento]
GO
ALTER TABLE [dbo].[Organizadores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Organizadores_dbo.Usuarios_Usuario_Nombre] FOREIGN KEY([Usuario_Nombre])
REFERENCES [dbo].[Usuarios] ([Nombre])
GO
ALTER TABLE [dbo].[Organizadores] CHECK CONSTRAINT [FK_dbo.Organizadores_dbo.Usuarios_Usuario_Nombre]
GO
ALTER TABLE [dbo].[ServicioContratados]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServicioContratados_dbo.Eventos_Evento_Nombre] FOREIGN KEY([Evento_Nombre])
REFERENCES [dbo].[Eventos] ([Nombre])
GO
ALTER TABLE [dbo].[ServicioContratados] CHECK CONSTRAINT [FK_dbo.ServicioContratados_dbo.Eventos_Evento_Nombre]
GO
ALTER TABLE [dbo].[ServicioContratados]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServicioContratados_dbo.Servicios_Rut_NombreServicio] FOREIGN KEY([Rut], [NombreServicio])
REFERENCES [dbo].[Servicios] ([Rut], [NombreServicio])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServicioContratados] CHECK CONSTRAINT [FK_dbo.ServicioContratados_dbo.Servicios_Rut_NombreServicio]
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Servicios_dbo.Proveedores_Rut] FOREIGN KEY([Rut])
REFERENCES [dbo].[Proveedores] ([Rut])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Servicios] CHECK CONSTRAINT [FK_dbo.Servicios_dbo.Proveedores_Rut]
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Servicios_dbo.TipoServicios_TipoServicio_NombreTipoServicio] FOREIGN KEY([TipoServicio_NombreTipoServicio])
REFERENCES [dbo].[TipoServicios] ([NombreTipoServicio])
GO
ALTER TABLE [dbo].[Servicios] CHECK CONSTRAINT [FK_dbo.Servicios_dbo.TipoServicios_TipoServicio_NombreTipoServicio]
GO
ALTER TABLE [dbo].[TipoEventoTipoServicios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TipoEventoTipoServicios_dbo.TipoEventos_TipoEvento_NombreTipoEvento] FOREIGN KEY([TipoEvento_NombreTipoEvento])
REFERENCES [dbo].[TipoEventos] ([NombreTipoEvento])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TipoEventoTipoServicios] CHECK CONSTRAINT [FK_dbo.TipoEventoTipoServicios_dbo.TipoEventos_TipoEvento_NombreTipoEvento]
GO
ALTER TABLE [dbo].[TipoEventoTipoServicios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TipoEventoTipoServicios_dbo.TipoServicios_TipoServicio_NombreTipoServicio] FOREIGN KEY([TipoServicio_NombreTipoServicio])
REFERENCES [dbo].[TipoServicios] ([NombreTipoServicio])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TipoEventoTipoServicios] CHECK CONSTRAINT [FK_dbo.TipoEventoTipoServicios_dbo.TipoServicios_TipoServicio_NombreTipoServicio]
GO
USE [master]
GO
ALTER DATABASE [ObligatorioP3MVC] SET  READ_WRITE 
GO
