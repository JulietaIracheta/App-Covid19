USE [master]
GO
/****** Object:  Database [TP-20201C]    Script Date: 21/6/2020 00:55:04 ******/
CREATE DATABASE [TP-20201C]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TP-20201C', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TP-20201C.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TP-20201C_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TP-20201C_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TP-20201C] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TP-20201C].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TP-20201C] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TP-20201C] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TP-20201C] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TP-20201C] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TP-20201C] SET ARITHABORT OFF 
GO
ALTER DATABASE [TP-20201C] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TP-20201C] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TP-20201C] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TP-20201C] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TP-20201C] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TP-20201C] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TP-20201C] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TP-20201C] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TP-20201C] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TP-20201C] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TP-20201C] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TP-20201C] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TP-20201C] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TP-20201C] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TP-20201C] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TP-20201C] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TP-20201C] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TP-20201C] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TP-20201C] SET  MULTI_USER 
GO
ALTER DATABASE [TP-20201C] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TP-20201C] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TP-20201C] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TP-20201C] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TP-20201C] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TP-20201C]
GO
/****** Object:  Table [dbo].[Denuncias]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Denuncias](
	[IdDenuncia] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidad] [int] NOT NULL,
	[IdMotivo] [int] NOT NULL,
	[Comentarios] [nvarchar](300) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Denuncias] PRIMARY KEY CLUSTERED 
(
	[IdDenuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonacionesInsumos]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionesInsumos](
	[IdDonacionInsumo] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidadDonacionInsumo] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_DonacionesInsumos] PRIMARY KEY CLUSTERED 
(
	[IdDonacionInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonacionesMonetarias]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionesMonetarias](
	[IdDonacionMonetaria] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidadDonacionMonetaria] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Dinero] [decimal](18, 2) NOT NULL,
	[ArchivoTransferencia] [nvarchar](200) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_DonacionesMonetarias] PRIMARY KEY CLUSTERED 
(
	[IdDonacionMonetaria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotivoDenuncia]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MotivoDenuncia](
	[IdMotivoDenuncia] [int] NOT NULL,
	[Descripcion] [varchar](30) NOT NULL,
 CONSTRAINT [PK_MotivoDenuncia] PRIMARY KEY CLUSTERED 
(
	[IdMotivoDenuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Necesidades]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Necesidades](
	[IdNecesidad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [text] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaFin] [datetime] NOT NULL,
	[TelefonoContacto] [nvarchar](30) NOT NULL,
	[TipoDonacion] [int] NOT NULL,
	[Foto] [nvarchar](100) NOT NULL,
	[IdUsuarioCreador] [int] NOT NULL,
	[Estado] [int] NOT NULL,
	[Valoracion] [decimal](18, 0) NULL DEFAULT ((0)),
 CONSTRAINT [PK_Necesidades] PRIMARY KEY CLUSTERED 
(
	[IdNecesidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NecesidadesDonacionesInsumos]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NecesidadesDonacionesInsumos](
	[IdNecesidadDonacionInsumo] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidad] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_NecesidadesDonacionesInsumos] PRIMARY KEY CLUSTERED 
(
	[IdNecesidadDonacionInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NecesidadesDonacionesMonetarias]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NecesidadesDonacionesMonetarias](
	[IdNecesidadDonacionMonetaria] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidad] [int] NOT NULL,
	[Dinero] [decimal](18, 2) NOT NULL,
	[CBU] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_NecesidadesDonacionesMonetarias] PRIMARY KEY CLUSTERED 
(
	[IdNecesidadDonacionMonetaria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NecesidadesReferencias]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NecesidadesReferencias](
	[IdReferencia] [int] IDENTITY(1,1) NOT NULL,
	[IdNecesidad] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NecesidadesReferencias] PRIMARY KEY CLUSTERED 
(
	[IdReferencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NecesidadesValoraciones]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NecesidadesValoraciones](
	[IdValoracion] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdNecesidad] [int] NOT NULL,
	[Valoracion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NecesidadesValoraciones] PRIMARY KEY CLUSTERED 
(
	[IdValoracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 21/6/2020 00:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Foto] [nvarchar](100) NULL,
	[TipoUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Denuncias]  WITH CHECK ADD  CONSTRAINT [FK_Denuncia_MotivoDenuncia] FOREIGN KEY([IdMotivo])
REFERENCES [dbo].[MotivoDenuncia] ([IdMotivoDenuncia])
GO
ALTER TABLE [dbo].[Denuncias] CHECK CONSTRAINT [FK_Denuncia_MotivoDenuncia]
GO
ALTER TABLE [dbo].[Denuncias]  WITH CHECK ADD  CONSTRAINT [FK_Denuncias_Necesidades] FOREIGN KEY([IdNecesidad])
REFERENCES [dbo].[Necesidades] ([IdNecesidad])
GO
ALTER TABLE [dbo].[Denuncias] CHECK CONSTRAINT [FK_Denuncias_Necesidades]
GO
ALTER TABLE [dbo].[Denuncias]  WITH CHECK ADD  CONSTRAINT [FK_Denuncias_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Denuncias] CHECK CONSTRAINT [FK_Denuncias_Usuarios]
GO
ALTER TABLE [dbo].[DonacionesInsumos]  WITH CHECK ADD  CONSTRAINT [FK_DonacionesInsumos_NecesidadesDonacionesInsumos] FOREIGN KEY([IdNecesidadDonacionInsumo])
REFERENCES [dbo].[NecesidadesDonacionesInsumos] ([IdNecesidadDonacionInsumo])
GO
ALTER TABLE [dbo].[DonacionesInsumos] CHECK CONSTRAINT [FK_DonacionesInsumos_NecesidadesDonacionesInsumos]
GO
ALTER TABLE [dbo].[DonacionesInsumos]  WITH CHECK ADD  CONSTRAINT [FK_DonacionesInsumos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[DonacionesInsumos] CHECK CONSTRAINT [FK_DonacionesInsumos_Usuarios]
GO
ALTER TABLE [dbo].[DonacionesMonetarias]  WITH CHECK ADD  CONSTRAINT [FK_DonacionesMonetarias_NecesidadesDonacionesMonetarias] FOREIGN KEY([IdNecesidadDonacionMonetaria])
REFERENCES [dbo].[NecesidadesDonacionesMonetarias] ([IdNecesidadDonacionMonetaria])
GO
ALTER TABLE [dbo].[DonacionesMonetarias] CHECK CONSTRAINT [FK_DonacionesMonetarias_NecesidadesDonacionesMonetarias]
GO
ALTER TABLE [dbo].[DonacionesMonetarias]  WITH CHECK ADD  CONSTRAINT [FK_DonacionesMonetarias_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[DonacionesMonetarias] CHECK CONSTRAINT [FK_DonacionesMonetarias_Usuarios]
GO
ALTER TABLE [dbo].[Necesidades]  WITH CHECK ADD  CONSTRAINT [FK_Necesidades_Usuarios] FOREIGN KEY([IdUsuarioCreador])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Necesidades] CHECK CONSTRAINT [FK_Necesidades_Usuarios]
GO
ALTER TABLE [dbo].[NecesidadesDonacionesInsumos]  WITH CHECK ADD  CONSTRAINT [FK_NecesidadesDonacionesInsumos_Necesidades] FOREIGN KEY([IdNecesidad])
REFERENCES [dbo].[Necesidades] ([IdNecesidad])
GO
ALTER TABLE [dbo].[NecesidadesDonacionesInsumos] CHECK CONSTRAINT [FK_NecesidadesDonacionesInsumos_Necesidades]
GO
ALTER TABLE [dbo].[NecesidadesDonacionesMonetarias]  WITH CHECK ADD  CONSTRAINT [FK_NecesidadesDonacionesMonetarias_Necesidades] FOREIGN KEY([IdNecesidad])
REFERENCES [dbo].[Necesidades] ([IdNecesidad])
GO
ALTER TABLE [dbo].[NecesidadesDonacionesMonetarias] CHECK CONSTRAINT [FK_NecesidadesDonacionesMonetarias_Necesidades]
GO
ALTER TABLE [dbo].[NecesidadesReferencias]  WITH CHECK ADD  CONSTRAINT [FK_NecesidadesReferencias_Necesidades] FOREIGN KEY([IdNecesidad])
REFERENCES [dbo].[Necesidades] ([IdNecesidad])
GO
ALTER TABLE [dbo].[NecesidadesReferencias] CHECK CONSTRAINT [FK_NecesidadesReferencias_Necesidades]
GO
ALTER TABLE [dbo].[NecesidadesValoraciones]  WITH CHECK ADD  CONSTRAINT [FK_NecesidadesValoraciones_Necesidades] FOREIGN KEY([IdNecesidad])
REFERENCES [dbo].[Necesidades] ([IdNecesidad])
GO
ALTER TABLE [dbo].[NecesidadesValoraciones] CHECK CONSTRAINT [FK_NecesidadesValoraciones_Necesidades]
GO
ALTER TABLE [dbo].[NecesidadesValoraciones]  WITH CHECK ADD  CONSTRAINT [FK_NecesidadesValoraciones_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[NecesidadesValoraciones] CHECK CONSTRAINT [FK_NecesidadesValoraciones_Usuarios]
GO
USE [master]
GO
ALTER DATABASE [TP-20201C] SET  READ_WRITE 
GO
USE [TP-20201C]
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (1, N'Fraude')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (2, N'Indebida')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (3, N'Violación de Derechos')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (4, N'Contiene información política')
GO
