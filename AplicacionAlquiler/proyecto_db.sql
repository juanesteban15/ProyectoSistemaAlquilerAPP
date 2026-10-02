//*
CREATE DATABASE proyecto_db
GO
use proyecto_db
GO

create table [Personas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(20) NOT NULL,
    [Apellido] NVARCHAR(20) NOT NULL,
    [Email] NVARCHAR(20) NOT NULL,
    [PaisNacimiento] NVARCHAR(100) NOT NULL,
    [FechaNacimiento] DateTime NOT NULL,
    [TipoDocumento] NVARCHAR(20) NOT NULL,
    [NumeroDocumento] NVARCHAR(20) Not NULL,
    [Genero] NVARCHAR(20) NOT NULL,
    [ComplementoPais] NVARCHAR(20) NOT NULL,
    [TELEFONO]  NVARCHAR(20) NOT NULL,
    )
   

   CREATE TABLE [EstadosUsuarios]
   (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (100) NOT NULL
   )




   create table [Usuarios]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Persona] INT NOT NULL REFERENCES [Personas] ([Id]),
    [NombreUsuario] NVARCHAR(20) NOT NULL,
    [Contrasena] NVARCHAR(20) NOT NULL,
    [Rol] NVARCHAR(100) NOT NULL,
    [EstadoUsuario] INT NOT NULL REFERENCES [Estadosusuarios]([Id]),
    [FechaCreacion] DATETIME DEFAULT GETDATE()
   )



   CREATE TABLE [EstadosVehiculos]
   (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL
   )

   CREATE TABLE[Tiposvehiculos]
      (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL
   )


   CREATE TABLE [Marcas]
   (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL,
    [TipoVehiculo] INT REFERENCES [Tiposvehiculos]([Id])
   )

   

   CREATE TABLE [ColoresVehiculos]
   (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL,

   )


      CREATE TABLE [SistemasTransmisiones]
   (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL,
    [TipoVehiculo] INT REFERENCES [Tiposvehiculos]([Id])
   )
   
   CREATE TABLE [Categorias]
    (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL,
    [TipoVehiculo] INT REFERENCES [Tiposvehiculos]([Id])

   )
   
   CREATE TABLE [TiposCombustibles]
       (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR (50) NOT NULL,
    [TipoVehiculo] INT REFERENCES [Tiposvehiculos](Id)
   )







      create table [Vehiculos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Placa] NVARCHAR(6) NOT NULL UNIQUE,
    [Propietario] INT NOT NULL REFERENCES [Personas] ([Id]),
    [EstadoVehiculo] INT NOT NULL REFERENCES [EstadosVehiculos] ([Id]),
    [TipoVehiculo] INT NOT  NULL REFERENCES [TiposVehiculos] ([Id]),
    [Marca] INT NOT NULL REFERENCES [Marcas]([Id]),
    [Color] INT  NOT NULL REFERENCES [ColoresVehiculos]([Id]),
    [SistemaTransmision] INT NOT NULL REFERENCES [SistemasTransmisiones]([Id]),
    [Categoria] INT NOT NULL REFERENCES [Categorias]([Id]),
    [TipoCombustibles] INT NOT NULL REFERENCES [TiposCombustibles]([Id]),
    [Modelo] INT NOT NULL,
    [FechaRegistro] DATETIME DEFAULT GETDATE()
   )





   CREATE TABLE [Tarifas]
   (
   [Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
   [Vehiculos] INT NOT NULL REFERENCES [Vehiculos]([Id])




   )



   ALTER TABLE [Tarifas]
   ADD [PrecioPorDia] DECIMAL NOT NULL
   ,[FechaInicio] DATETIME ,[ACTIVA] BIT NOT NULL;

   
   CREATE TABLE [EstadosReservas]
   (
   [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
   [Nombree] NVARCHAR   (100)
   )


   CREATE TABLE [MetodosPagos]
   (
   [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1) ,
   [Nombre] NVARCHAR NOT NULL
   )




   CREATE TABLE [Reservas]
   (
       [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
       [Vehiculo] INT NOT NULL REFERENCES [Vehiculos]([Id]),
       [Cliente] INT NOT NULL REFERENCES [Usuarios]([Id]),
       [EstadoReserva] INT NOT NULL REFERENCES [EstadosReservas]([Id]),
       [FechaInicio] DATETIME NOT NULL,
       [FechaFiN] DateTime NOT NULL,
       [MetodoPago] INT NOT NULL REFERENCES [MetodosPagos]([Id]),
       [Tarifa] INT NOT NULL REFERENCES [Tarifas]([Id]),
       [PrecioTotal] DECIMAL (11,5) ,
       [Observiones] NVARCHAR(200),
       [FechaCreacion] DATETIME DEFAULT GETDATE() 
   
   )
   //*