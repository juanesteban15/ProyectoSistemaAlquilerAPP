using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicacionAlquiler.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColoresVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColoresVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosReservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosReservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetodosPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosPagos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplementoPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroAuditoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Movimiento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCombustibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCombustibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Persona = table.Column<int>(type: "int", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estadousuario = table.Column<int>(type: "int", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_EstadosUsuarios_Estadousuario",
                        column: x => x.Estadousuario,
                        principalTable: "EstadosUsuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_Persona",
                        column: x => x.Persona,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_TiposVehiculos_TipoVehiculo",
                        column: x => x.TipoVehiculo,
                        principalTable: "TiposVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcas_TiposVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TiposVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SistemasTransmisiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemasTransmisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SistemasTransmisiones_TiposVehiculos_TipoVehiculo",
                        column: x => x.TipoVehiculo,
                        principalTable: "TiposVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Propietario = table.Column<int>(type: "int", nullable: true),
                    EstadoVehiculo = table.Column<int>(type: "int", nullable: true),
                    TipoVehiculo = table.Column<int>(type: "int", nullable: true),
                    Marca = table.Column<int>(type: "int", nullable: true),
                    ColorVehiculo = table.Column<int>(type: "int", nullable: true),
                    SistemaTransmision = table.Column<int>(type: "int", nullable: true),
                    Categoria = table.Column<int>(type: "int", nullable: true),
                    TipoCombustible = table.Column<int>(type: "int", nullable: true),
                    Modelo = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Categorias_Categoria",
                        column: x => x.Categoria,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_ColoresVehiculos_ColorVehiculo",
                        column: x => x.ColorVehiculo,
                        principalTable: "ColoresVehiculos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_EstadosVehiculos_EstadoVehiculo",
                        column: x => x.EstadoVehiculo,
                        principalTable: "EstadosVehiculos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_Marcas_Marca",
                        column: x => x.Marca,
                        principalTable: "Marcas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_SistemasTransmisiones_SistemaTransmision",
                        column: x => x.SistemaTransmision,
                        principalTable: "SistemasTransmisiones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_TiposCombustibles_TipoCombustible",
                        column: x => x.TipoCombustible,
                        principalTable: "TiposCombustibles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_TiposVehiculos_TipoVehiculo",
                        column: x => x.TipoVehiculo,
                        principalTable: "TiposVehiculos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehiculos_Usuarios_Propietario",
                        column: x => x.Propietario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vehiculo = table.Column<int>(type: "int", nullable: false),
                    PrecioPorDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarifas_Vehiculos_Vehiculo",
                        column: x => x.Vehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vehiculo = table.Column<int>(type: "int", nullable: false),
                    Cliente = table.Column<int>(type: "int", nullable: false),
                    EstadoReserva = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<int>(type: "int", nullable: false),
                    Tarifa = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_EstadosReservas_EstadoReserva",
                        column: x => x.EstadoReserva,
                        principalTable: "EstadosReservas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservas_MetodosPagos_MetodoPago",
                        column: x => x.MetodoPago,
                        principalTable: "MetodosPagos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservas_Tarifas_Tarifa",
                        column: x => x.Tarifa,
                        principalTable: "Tarifas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservas_Vehiculos_Vehiculo",
                        column: x => x.Vehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_TipoVehiculo",
                table: "Categorias",
                column: "TipoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_TipoVehiculoId",
                table: "Marcas",
                column: "TipoVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Cliente",
                table: "Reservas",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_EstadoReserva",
                table: "Reservas",
                column: "EstadoReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_MetodoPago",
                table: "Reservas",
                column: "MetodoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Tarifa",
                table: "Reservas",
                column: "Tarifa");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Vehiculo",
                table: "Reservas",
                column: "Vehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_SistemasTransmisiones_TipoVehiculo",
                table: "SistemasTransmisiones",
                column: "TipoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifas_Vehiculo",
                table: "Tarifas",
                column: "Vehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Estadousuario",
                table: "Usuarios",
                column: "Estadousuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Persona",
                table: "Usuarios",
                column: "Persona");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Categoria",
                table: "Vehiculos",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ColorVehiculo",
                table: "Vehiculos",
                column: "ColorVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_EstadoVehiculo",
                table: "Vehiculos",
                column: "EstadoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Marca",
                table: "Vehiculos",
                column: "Marca");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Propietario",
                table: "Vehiculos",
                column: "Propietario");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_SistemaTransmision",
                table: "Vehiculos",
                column: "SistemaTransmision");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TipoCombustible",
                table: "Vehiculos",
                column: "TipoCombustible");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TipoVehiculo",
                table: "Vehiculos",
                column: "TipoVehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroAuditoria");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "EstadosReservas");

            migrationBuilder.DropTable(
                name: "MetodosPagos");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ColoresVehiculos");

            migrationBuilder.DropTable(
                name: "EstadosVehiculos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "SistemasTransmisiones");

            migrationBuilder.DropTable(
                name: "TiposCombustibles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TiposVehiculos");

            migrationBuilder.DropTable(
                name: "EstadosUsuarios");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
