
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
    [TipoDocumento] NVARCHAR(100) NOT NULL,
    [NumeroDocumento] NVARCHAR(100) NOT NULL,
    [Genero] NVARCHAR(100) NOT NULL,
    [ComplementoPais] NVARCHAR(50) NOT NULL,   -- igual que la propiedad C# (CompletoPais)
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


SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    -- Personas
    DECLARE @Persona1 INT, @Persona2 INT;
    INSERT INTO Personas (Nombre, Apellido, Email, PaisNacimiento, FechaNacimiento, TipoDocumento, NumeroDocumento, Genero, ComplementoPais, Telefono)
    VALUES
    (N'Camilo', N'Perez', N'camilo@gmail.com', N'Colombia', '2004-09-15', N'Cedula', N'121213', N'Hombre', N'+57', N'3214213053');
    SET @Persona1 = SCOPE_IDENTITY();

    INSERT INTO Personas (Nombre, Apellido, Email, PaisNacimiento, FechaNacimiento, TipoDocumento, NumeroDocumento, Genero, ComplementoPais, Telefono)
    VALUES
    (N'Juan', N'Daza', N'daza@gmail.com', N'Colombia', '2004-09-15', N'Cedula', N'1234567', N'Hombre', N'+57', N'3214213051');
    SET @Persona2 = SCOPE_IDENTITY();

    -- EstadosUsuarios
    DECLARE @EstadoActivo INT, @EstadoNoActivo INT;
    INSERT INTO EstadosUsuarios (Nombre) VALUES (N'Activo'); SET @EstadoActivo = SCOPE_IDENTITY();
    INSERT INTO EstadosUsuarios (Nombre) VALUES (N'No activo'); SET @EstadoNoActivo = SCOPE_IDENTITY();

    -- Usuarios (usar cadenas para contraseñas / en producción guardar hashes)
    DECLARE @Usuario1 INT, @Usuario2 INT;
    INSERT INTO Usuarios (Persona, NombreUsuario, Contrasena, Rol, Estadousuario)
    VALUES (@Persona1, N'CAMILA', N'123456', N'USUARIO', @EstadoActivo);
    SET @Usuario1 = SCOPE_IDENTITY();

    INSERT INTO Usuarios (Persona, NombreUsuario, Contrasena, Rol, Estadousuario)
    VALUES (@Persona2, N'CJ', N'123456', N'USUARIO', @EstadoActivo);
    SET @Usuario2 = SCOPE_IDENTITY();

    -- Catálogos mínimos (orden correcto: TiposVehiculos antes de Marcas/Sistemas/Categorias/Combustible)
    DECLARE @EstVehDisponible INT, @TipoCarro INT, @MarcaToyota INT, @ColorBlanco INT;
    DECLARE @TransAuto INT, @CatSedan INT, @CombGasolina INT, @EstResConfirmada INT, @PagoTarjeta INT;

    INSERT INTO EstadosVehiculos (Nombre) VALUES (N'Disponible'); SET @EstVehDisponible = SCOPE_IDENTITY();

    INSERT INTO TiposVehiculos (Nombre) VALUES (N'Carro'); SET @TipoCarro = SCOPE_IDENTITY();

    INSERT INTO Marcas (Nombre, TipoVehiculoId) VALUES (N'Toyota', @TipoCarro); SET @MarcaToyota = SCOPE_IDENTITY();

    INSERT INTO ColoresVehiculos (Nombre) VALUES (N'Blanco'); SET @ColorBlanco = SCOPE_IDENTITY();

    INSERT INTO SistemasTransmisiones (Nombre, TipoVehiculo) VALUES (N'Automática', @TipoCarro); SET @TransAuto = SCOPE_IDENTITY();

    INSERT INTO Categorias (Nombre, TipoVehiculo) VALUES (N'Sedán', @TipoCarro); SET @CatSedan = SCOPE_IDENTITY();

    INSERT INTO TiposCombustibles (Nombre, TipoVehiculo) VALUES (N'Gasolina', @TipoCarro); SET @CombGasolina = SCOPE_IDENTITY();

    INSERT INTO EstadosReservas (Nombre) VALUES (N'Confirmada'); SET @EstResConfirmada = SCOPE_IDENTITY();

    INSERT INTO MetodosPagos (Nombre) VALUES (N'Tarjeta de crédito'); SET @PagoTarjeta = SCOPE_IDENTITY();

    -- Vehículo (una sola inserción; Placa debe ser única)
    DECLARE @Vehiculo1 INT;
    INSERT INTO Vehiculos (Placa, Propietario, EstadoVehiculo, TipoVehiculo, Marca, ColorVehiculo, SistemaTransmision, Categoria, TipoCombustible, Modelo)
    VALUES (N'ABC123', @Usuario1, @EstVehDisponible, @TipoCarro, @MarcaToyota, @ColorBlanco, @TransAuto, @CatSedan, @CombGasolina, 2022);
    SET @Vehiculo1 = SCOPE_IDENTITY();

    -- Tarifa
    DECLARE @Tarifa1 INT;
    INSERT INTO Tarifas (Vehiculo, PrecioPorDia, FechaInicio, Activa)
    VALUES (@Vehiculo1, 150000.00, GETDATE(), 1);
    SET @Tarifa1 = SCOPE_IDENTITY();

    -- Reserva (cliente = usuario2)
    INSERT INTO Reservas (Vehiculo, Cliente, EstadoReserva, FechaInicio, FechaFin, MetodoPago, Tarifa, PrecioTotal, Observaciones)
    VALUES (@Vehiculo1, @Usuario2, @EstResConfirmada, '2026-10-10', '2026-10-13', @PagoTarjeta, @Tarifa1, 450000.00, N'Reserva de prueba');

    -- (Opcional) marcar vehículo como reservado
    UPDATE Vehiculos SET EstadoVehiculo = (SELECT TOP 1 Id FROM EstadosVehiculos WHERE Nombre = N'Reservado') 
    WHERE Id = @Vehiculo1 AND EXISTS (SELECT 1 FROM EstadosVehiculos WHERE Nombre = N'Reservado');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

select *from Personas

select*from Usuarios

select*from EstadosUsuarios
/*