create  database CreditoAuto
go
use CreditoAuto
go
create table dbo.Marca(
MarcaId int identity(1,1),
Descripcion varchar(50),
primary key(MarcaId)
)
GO
create table dbo.Cliente(
	Identificacion varchar(10) NOT NULL,
	Nombres varchar(50) NOT NULL,
	Edad int NOT NULL,
	FechaNacimiento date NOT NULL,
	Apellidos varchar(50) NOT NULL,
	Direccion varchar(50) NOT NULL,
	Telefono varchar(10) NOT NULL,
	EstadoCivil varchar(50) NOT NULL,
	IdentificacionConyugue varchar(10) NOT NULL,
	NombreConyugue varchar(50) NOT NULL,
	SujetoCredito varchar(50) NULL,
	CONSTRAINT PK_Identificacion PRIMARY KEY CLUSTERED 
	(
		Identificacion ASC
	)
)
go
create table dbo.Patio
(
	Nombre varchar(50) NOT NULL,
	Direccion varchar(50) NOT NULL,
	Telefono varchar(10) NOT NULL,
	NumeroPuntoVenta int NOT NULL,
 CONSTRAINT PK_NumeroPuntoVenta PRIMARY KEY CLUSTERED (
	NumeroPuntoVenta ASC
)
)
GO
CREATE TABLE dbo.Ejecutivo(
	Identificacion varchar(10) NOT NULL,
	Nombres varchar(250) NOT NULL,
	Apellidos varchar(250) NOT NULL,
	Direccion varchar(250) NOT NULL,
	TelefonoConvencional varchar(15) NOT NULL,
	Celular varchar(15) NOT NULL,
	NumeroPuntoVenta int NOT NULL,
	Edad int NOT NULL,
 CONSTRAINT PK_Ejecutivo PRIMARY KEY CLUSTERED 
(
	Identificacion ASC
))
GO
ALTER TABLE [dbo].[Ejecutivo]  WITH CHECK ADD  CONSTRAINT [FK_Ejecutivo_Patio] FOREIGN KEY([NumeroPuntoVenta])
REFERENCES [dbo].[Patio] ([NumeroPuntoVenta])
GO
ALTER TABLE [dbo].[Ejecutivo] CHECK CONSTRAINT [FK_Ejecutivo_Patio]
GO
CREATE TABLE dbo.AsignacionCliente(
	AsignacionId int IDENTITY(1,1) NOT NULL,
	Identificacion varchar(10) NOT NULL,
	NumeroPuntoVenta int NOT NULL,
	FechaAsignacion datetime NOT NULL,
 CONSTRAINT PK_Asignacioncliente PRIMARY KEY CLUSTERED 
(
	AsignacionId ASC
)
)
GO
ALTER TABLE [dbo].[AsignacionCliente]  WITH CHECK ADD  CONSTRAINT [FK_AsignacionCliente_Cliente] FOREIGN KEY([Identificacion])
REFERENCES [dbo].[Cliente] ([Identificacion])
GO
ALTER TABLE [dbo].[AsignacionCliente] CHECK CONSTRAINT [FK_AsignacionCliente_Cliente]
GO
ALTER TABLE [dbo].[AsignacionCliente]  WITH CHECK ADD  CONSTRAINT [FK_AsignacionCliente_Patio] FOREIGN KEY([NumeroPuntoVenta])
REFERENCES [dbo].[Patio] ([NumeroPuntoVenta ])
GO
ALTER TABLE [dbo].[AsignacionCliente] CHECK CONSTRAINT [FK_AsignacionCliente_Patio]
GO
CREATE TABLE [dbo].[Vehiculo](
	Placa varchar(8) NOT NULL,
	Modelo varchar (50) NOT NULL,
	NumeroChasis varchar(50) NOT NULL,
	MarcaId int NOT NULL,
	Tipo varchar(50) NOT NULL,
	Cilindraje decimal(18,2) NOT NULL,
	Avaluo money NOT NULL,
 CONSTRAINT PK_Vehiculo PRIMARY KEY CLUSTERED 
(
	Placa ASC
)
)
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculo_Marca] FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marca] ([MarcaId])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Marca]
GO
