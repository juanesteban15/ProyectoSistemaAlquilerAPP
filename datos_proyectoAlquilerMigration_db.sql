-- Datos exportados de proyectoAlquilerMigration_db. Ejecutar después del script de estructura en una base vacía.
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- Personas: 2 registros
SET IDENTITY_INSERT [dbo].[Personas] ON;
INSERT INTO [dbo].[Personas] ([Id], [Nombre], [Apellido], [Email], [PaisNacimiento], [FechaNacimiento], [TipoDocumento], [NumeroDocumento], [Genero], [ComplementoPais], [Telefono]) VALUES (3, N'Carlos', N'Propietario', N'carlos.demo@example.com', N'Colombia', '1990-05-10T00:00:00.0000000', N'CC', N'99000001', N'Masculino', N'+57', N'3000000001');
INSERT INTO [dbo].[Personas] ([Id], [Nombre], [Apellido], [Email], [PaisNacimiento], [FechaNacimiento], [TipoDocumento], [NumeroDocumento], [Genero], [ComplementoPais], [Telefono]) VALUES (4, N'Laura', N'Cliente', N'laura.demo@example.com', N'Colombia', '1995-08-20T00:00:00.0000000', N'CC', N'99000002', N'Femenino', N'+57', N'3000000002');
SET IDENTITY_INSERT [dbo].[Personas] OFF;

-- EstadosUsuarios: 2 registros
SET IDENTITY_INSERT [dbo].[EstadosUsuarios] ON;
INSERT INTO [dbo].[EstadosUsuarios] ([Id], [Nombre]) VALUES (2, N'Activo');
INSERT INTO [dbo].[EstadosUsuarios] ([Id], [Nombre]) VALUES (3, N'Inactivo');
SET IDENTITY_INSERT [dbo].[EstadosUsuarios] OFF;

-- EstadosVehiculos: 4 registros
SET IDENTITY_INSERT [dbo].[EstadosVehiculos] ON;
INSERT INTO [dbo].[EstadosVehiculos] ([Id], [Nombre]) VALUES (2, N'Disponible');
INSERT INTO [dbo].[EstadosVehiculos] ([Id], [Nombre]) VALUES (3, N'Reservado');
INSERT INTO [dbo].[EstadosVehiculos] ([Id], [Nombre]) VALUES (4, N'Mantenimiento');
INSERT INTO [dbo].[EstadosVehiculos] ([Id], [Nombre]) VALUES (5, N'Inactivo');
SET IDENTITY_INSERT [dbo].[EstadosVehiculos] OFF;

-- EstadosReservas: 5 registros
SET IDENTITY_INSERT [dbo].[EstadosReservas] ON;
INSERT INTO [dbo].[EstadosReservas] ([Id], [Nombre]) VALUES (2, N'Pendiente');
INSERT INTO [dbo].[EstadosReservas] ([Id], [Nombre]) VALUES (3, N'Confirmada');
INSERT INTO [dbo].[EstadosReservas] ([Id], [Nombre]) VALUES (4, N'Denegada');
INSERT INTO [dbo].[EstadosReservas] ([Id], [Nombre]) VALUES (5, N'Cancelada');
INSERT INTO [dbo].[EstadosReservas] ([Id], [Nombre]) VALUES (6, N'Finalizada');
SET IDENTITY_INSERT [dbo].[EstadosReservas] OFF;

-- TiposVehiculos: 2 registros
SET IDENTITY_INSERT [dbo].[TiposVehiculos] ON;
INSERT INTO [dbo].[TiposVehiculos] ([Id], [Nombre]) VALUES (2, N'Carro');
INSERT INTO [dbo].[TiposVehiculos] ([Id], [Nombre]) VALUES (3, N'Motocicleta');
SET IDENTITY_INSERT [dbo].[TiposVehiculos] OFF;

-- ColoresVehiculos: 6 registros
SET IDENTITY_INSERT [dbo].[ColoresVehiculos] ON;
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (2, N'Blanco');
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (3, N'Negro');
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (4, N'Gris');
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (5, N'Plata');
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (6, N'Rojo');
INSERT INTO [dbo].[ColoresVehiculos] ([Id], [Nombre]) VALUES (7, N'Azul');
SET IDENTITY_INSERT [dbo].[ColoresVehiculos] OFF;

-- MetodosPagos: 3 registros
SET IDENTITY_INSERT [dbo].[MetodosPagos] ON;
INSERT INTO [dbo].[MetodosPagos] ([Id], [Nombre]) VALUES (2, N'Efectivo');
INSERT INTO [dbo].[MetodosPagos] ([Id], [Nombre]) VALUES (3, N'Tarjeta de crédito');
INSERT INTO [dbo].[MetodosPagos] ([Id], [Nombre]) VALUES (4, N'Transferencia bancaria');
SET IDENTITY_INSERT [dbo].[MetodosPagos] OFF;

-- TiposCombustibles: 6 registros
SET IDENTITY_INSERT [dbo].[TiposCombustibles] ON;
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (2, N'Gasolina', 2);
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (3, N'Diesel', 2);
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (4, N'Híbrido', 2);
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (5, N'Eléctrico', 2);
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (6, N'Gasolina', 3);
INSERT INTO [dbo].[TiposCombustibles] ([Id], [Nombre], [TipoVehiculo]) VALUES (7, N'Eléctrico', 3);
SET IDENTITY_INSERT [dbo].[TiposCombustibles] OFF;

-- Categorias: 8 registros
SET IDENTITY_INSERT [dbo].[Categorias] ON;
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (2, N'Sedán', 2);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (3, N'Hatchback', 2);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (4, N'SUV', 2);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (5, N'Camioneta', 2);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (6, N'Scooter', 3);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (7, N'Naked', 3);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (8, N'Deportiva', 3);
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [TipoVehiculo]) VALUES (9, N'Touring', 3);
SET IDENTITY_INSERT [dbo].[Categorias] OFF;

-- Marcas: 11 registros
SET IDENTITY_INSERT [dbo].[Marcas] ON;
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (2, N'Toyota', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (3, N'Chevrolet', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (4, N'Renault', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (5, N'Mazda', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (6, N'Nissan', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (7, N'Kia', 2);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (8, N'Honda', 3);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (9, N'Yamaha', 3);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (10, N'Suzuki', 3);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (11, N'AKT', 3);
INSERT INTO [dbo].[Marcas] ([Id], [Nombre], [TipoVehiculoId]) VALUES (12, N'Bajaj', 3);
SET IDENTITY_INSERT [dbo].[Marcas] OFF;

-- SistemasTransmisiones: 5 registros
SET IDENTITY_INSERT [dbo].[SistemasTransmisiones] ON;
INSERT INTO [dbo].[SistemasTransmisiones] ([Id], [Nombre], [TipoVehiculo]) VALUES (2, N'Manual', 2);
INSERT INTO [dbo].[SistemasTransmisiones] ([Id], [Nombre], [TipoVehiculo]) VALUES (3, N'Automática', 2);
INSERT INTO [dbo].[SistemasTransmisiones] ([Id], [Nombre], [TipoVehiculo]) VALUES (4, N'CVT', 2);
INSERT INTO [dbo].[SistemasTransmisiones] ([Id], [Nombre], [TipoVehiculo]) VALUES (5, N'Manual', 3);
INSERT INTO [dbo].[SistemasTransmisiones] ([Id], [Nombre], [TipoVehiculo]) VALUES (6, N'Automática', 3);
SET IDENTITY_INSERT [dbo].[SistemasTransmisiones] OFF;

-- Usuarios: 2 registros
SET IDENTITY_INSERT [dbo].[Usuarios] ON;
INSERT INTO [dbo].[Usuarios] ([Id], [Persona], [NombreUsuario], [Contrasena], [Rol], [Estadousuario], [FechaEliminacion], [FechaCreacion], [FotoPerfilRuta]) VALUES (4, 3, N'propietario_demo', N'Demo123!', N'Propietario', 2, NULL, NULL, NULL);
INSERT INTO [dbo].[Usuarios] ([Id], [Persona], [NombreUsuario], [Contrasena], [Rol], [Estadousuario], [FechaEliminacion], [FechaCreacion], [FotoPerfilRuta]) VALUES (5, 4, N'cliente_demo', N'Demo123!', N'Cliente', 2, NULL, NULL, NULL);
SET IDENTITY_INSERT [dbo].[Usuarios] OFF;

-- Vehiculos: 1 registros
SET IDENTITY_INSERT [dbo].[Vehiculos] ON;
INSERT INTO [dbo].[Vehiculos] ([Id], [Placa], [Propietario], [EstadoVehiculo], [TipoVehiculo], [Marca], [ColorVehiculo], [SistemaTransmision], [Categoria], [TipoCombustible], [Modelo], [FechaRegistro], [Estado], [FechaEliminacion]) VALUES (4, N'DEM123', 4, 2, 2, 2, 2, 3, 2, 2, 2022, '2026-10-14T16:50:48.7033333', 1, NULL);
SET IDENTITY_INSERT [dbo].[Vehiculos] OFF;

-- Tarifas: 2 registros
SET IDENTITY_INSERT [dbo].[Tarifas] ON;
INSERT INTO [dbo].[Tarifas] ([Id], [Vehiculo], [PrecioPorDia], [FechaInicio], [Activa]) VALUES (1, 4, 120000.00, '2026-10-01T00:00:00.0000000', 0);
INSERT INTO [dbo].[Tarifas] ([Id], [Vehiculo], [PrecioPorDia], [FechaInicio], [Activa]) VALUES (2, 4, 150000.00, '2026-10-14T00:00:00.0000000', 1);
SET IDENTITY_INSERT [dbo].[Tarifas] OFF;

-- Reservas: 1 registros
SET IDENTITY_INSERT [dbo].[Reservas] ON;
INSERT INTO [dbo].[Reservas] ([Id], [Vehiculo], [Cliente], [EstadoReserva], [FechaInicio], [FechaFin], [MetodoPago], [Tarifa], [PrecioTotal], [Observaciones], [FechaCreacion]) VALUES (4, 4, 5, 3, '2026-10-15T00:00:00.0000000', '2026-10-17T00:00:00.0000000', 3, 2, 300000.00, N'Reserva de demostración', '2026-10-14T16:50:48.7200000');
SET IDENTITY_INSERT [dbo].[Reservas] OFF;

-- RegistroAuditoria: 57 registros
SET IDENTITY_INSERT [dbo].[RegistroAuditoria] ON;
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (1, '2026-10-14T22:59:01.4187478', N'CONSULTA', N'Se consultaron las personas');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (2, '2026-10-14T23:03:45.8265908', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (3, '2026-10-14T23:04:34.0547642', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (4, '2026-10-14T23:04:42.8745080', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (5, '2026-10-14T23:04:55.0387929', N'CONSULTA', N'Se consultaron las personas. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (6, '2026-10-14T23:05:19.9363663', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (7, '2026-10-14T23:06:12.5053506', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (8, '2026-10-14T23:07:50.5849574', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (9, '2026-10-14T23:08:11.1854355', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (10, '2026-10-15T11:25:36.8132832', N'CONSULTA', N'Se consultaron las personas. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (11, '2026-10-15T11:38:20.0204836', N'CONSULTA', N'Se consultaron las personas. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (12, '2026-10-15T11:38:31.4826852', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (13, '2026-10-15T11:41:01.4008212', N'CONSULTA', N'Se consultaron las personas. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (14, '2026-10-15T11:41:15.7360068', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (15, '2026-10-15T11:44:18.1743125', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (16, '2026-10-15T11:44:55.0987688', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (17, '2026-10-23T22:58:14.1536484', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (18, '2026-10-23T23:47:32.0556686', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (19, '2026-10-23T23:58:53.9016909', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (20, '2026-10-23T23:59:02.3911216', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (21, '2026-10-23T23:59:14.4518428', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (22, '2026-10-24T00:00:17.8767943', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (23, '2026-10-24T00:01:54.2034878', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (24, '2026-10-24T00:01:55.9331693', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (25, '2026-10-24T00:02:06.8760011', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (26, '2026-10-24T22:50:59.8205635', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (27, '2026-10-24T22:51:03.4536323', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (28, '2026-10-24T22:51:05.5940762', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (29, '2026-10-24T22:51:06.6160319', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (30, '2026-10-24T22:51:07.3945669', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (31, '2026-10-24T22:51:09.1124321', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (32, '2026-10-24T22:51:14.2162194', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (33, '2026-10-24T22:51:15.3516118', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (34, '2026-10-24T22:51:16.9725183', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (35, '2026-10-24T22:51:21.1820855', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (36, '2026-10-24T22:51:25.8673208', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (37, '2026-10-24T22:51:29.5620327', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (38, '2026-10-24T22:51:35.9370385', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (39, '2026-10-24T22:51:40.4716181', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (40, '2026-10-24T22:51:41.5191371', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (41, '2026-10-24T22:51:47.3196821', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (42, '2026-10-24T22:51:49.3856106', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (43, '2026-10-24T22:51:51.2584501', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (44, '2026-10-24T22:55:06.7724895', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (45, '2026-10-24T22:55:07.9651200', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (46, '2026-10-24T22:55:09.0760382', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (47, '2026-10-24T22:55:09.6756683', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (48, '2026-10-24T22:55:13.2633153', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (49, '2026-10-24T22:55:13.8473015', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (50, '2026-10-24T22:55:14.5830097', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (51, '2026-10-24T22:55:18.5237130', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (52, '2026-10-24T22:55:55.8735213', N'CONSULTA', N'Se consultaron las reservas. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (53, '2026-10-24T22:55:58.4250958', N'CONSULTA', N'Se consultaron los usuarios. Cantidad: 2');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (54, '2026-10-24T22:55:59.6347111', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (55, '2026-10-24T22:56:22.5210361', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (56, '2026-10-24T23:49:07.5046591', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
INSERT INTO [dbo].[RegistroAuditoria] ([Id], [FechaUtc], [Movimiento], [Detalle]) VALUES (57, '2026-10-24T23:49:16.9087972', N'CONSULTA', N'Se consultaron los vehículos. Cantidad: 1');
SET IDENTITY_INSERT [dbo].[RegistroAuditoria] OFF;
COMMIT TRANSACTION;
