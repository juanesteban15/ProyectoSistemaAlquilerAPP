using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.implementaciones;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opciones =>
{
    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<IRegistroAuditoriaAplicacion, RegistroAuditoriasAplicacion>();
builder.Services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
builder.Services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
builder.Services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
builder.Services.AddScoped<ITarifasAplicacion, TarifasAplicacion>();
builder.Services.AddScoped<ICategoriasAplicacion, CategoriasAplicacion>();
builder.Services.AddScoped<IColoresVehiculosAplicacion, ColoresVehiculosAplicacion>();
builder.Services.AddScoped<IEstadosReservasAplicacion, EstadosReservasAplicacion>();
builder.Services.AddScoped<IEstadosUsuariosAplicacion, EstadosUsuariosAplicacion>();
builder.Services.AddScoped<IEstadosVehiculosAplicacion, EstadosVehiculosAplicacion>();
builder.Services.AddScoped<IMarcasAplicacion, MarcasAplicacion>();
builder.Services.AddScoped<IMetodosPagosAplicacion, MetodosPagosAplicacion>();
builder.Services.AddScoped<ISistemasTransmisionesAplicacion, SistemasTransmisionesAplicacion>();
builder.Services.AddScoped<ITiposCombustiblesAplicacion, TiposCombustiblesAplicacion>();
builder.Services.AddScoped<ITiposVehiculosAplicacion, TiposVehiculosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
