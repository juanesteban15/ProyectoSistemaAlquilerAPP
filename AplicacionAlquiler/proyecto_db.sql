
/*
CREATE DATABASE proyecto_db
GO
USE proyecto_db
GO

-- ===================== PERSONAS / USUARIOS =====================

CREATE TABLE [Personas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [Apellido] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [PaisNacimiento] NVARCHAR(100) NOT NULL,
    [FechaNacimiento] DATETIME NOT NULL,
    [TipoDocumento] NVARCHAR(20) NOT NULL,
    [NumeroDocumento] NVARCHAR(20) NOT NULL,
    [Genero] NVARCHAR(20) NOT NULL,
    [CompletoPais] NVARCHAR(50) NOT NULL,   -- igual que la propiedad C# (CompletoPais)
    [Telefono] NVARCHAR(20) NOT NULL
)

CREATE TABLE [EstadosUsuarios]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL
)

CREATE TABLE [Usuarios]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Persona] INT NOT NULL REFERENCES [Personas]([Id]),
    [NombreUsuario] NVARCHAR(50) NOT NULL,
    [Contrasena] NVARCHAR(200) NOT NULL,     -- espacio para guardar un hash
    [Rol] NVARCHAR(100) NOT NULL,
    [Estadousuario] INT NOT NULL REFERENCES [EstadosUsuarios]([Id]),
    [FechaCreacion] DATETIME DEFAULT GETDATE()
)

-- ===================== CATALOGOS =====================

CREATE TABLE [EstadosVehiculos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL
)

CREATE TABLE [TiposVehiculos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Marcas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [TipoVehiculoId] INT NOT NULL REFERENCES [TiposVehiculos]([Id])  -- igual que C# (TipoVehiculoId)
)

CREATE TABLE [ColoresVehiculos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL
)

CREATE TABLE [SistemasTransmisiones]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [TipoVehiculo] INT NOT NULL REFERENCES [TiposVehiculos]([Id])
)

CREATE TABLE [Categorias]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [TipoVehiculo] INT NOT NULL REFERENCES [TiposVehiculos]([Id])
)

CREATE TABLE [TiposCombustibles]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [TipoVehiculo] INT NOT NULL REFERENCES [TiposVehiculos]([Id])
)

CREATE TABLE [EstadosReservas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL
)

CREATE TABLE [MetodosPagos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(50) NOT NULL
)

-- ===================== VEHICULOS / TARIFAS / RESERVAS =====================

CREATE TABLE [Vehiculos]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Placa] NVARCHAR(6) NOT NULL UNIQUE,
    [Propietario] INT NOT NULL REFERENCES [Usuarios]([Id]),          -- C# apunta a Usuarios
    [EstadoVehiculo] INT NOT NULL REFERENCES [EstadosVehiculos]([Id]),
    [TipoVehiculo] INT NOT NULL REFERENCES [TiposVehiculos]([Id]),
    [Marca] INT NOT NULL REFERENCES [Marcas]([Id]),
    [ColorVehiculo] INT NOT NULL REFERENCES [ColoresVehiculos]([Id]),
    [SistemaTransmision] INT NOT NULL REFERENCES [SistemasTransmisiones]([Id]),
    [Categoria] INT NOT NULL REFERENCES [Categorias]([Id]),
    [TipoCombustible] INT NOT NULL REFERENCES [TiposCombustibles]([Id]),
    [Modelo] INT NOT NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [Tarifas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Vehiculo] INT NOT NULL REFERENCES [Vehiculos]([Id]),
    [PrecioPorDia] DECIMAL(18, 2) NOT NULL,
    [FechaInicio] DATETIME NOT NULL DEFAULT GETDATE(),
    [Activa] BIT NOT NULL
)

CREATE TABLE [Reservas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Vehiculo] INT NOT NULL REFERENCES [Vehiculos]([Id]),
    [Cliente] INT NOT NULL REFERENCES [Usuarios]([Id]),
    [EstadoReserva] INT NOT NULL REFERENCES [EstadosReservas]([Id]),
    [FechaInicio] DATETIME NOT NULL,
    [FechaFin] DATETIME NOT NULL,
    [MetodoPago] INT NOT NULL REFERENCES [MetodosPagos]([Id]),
    [Tarifa] INT NOT NULL REFERENCES [Tarifas]([Id]),
    [PrecioTotal] DECIMAL(18, 2) NOT NULL,
    [Observaciones] NVARCHAR(200) NULL,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE()
)
GO
/*