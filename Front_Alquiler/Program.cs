using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Implementaciones;
using Presentacion_CSN.Interfaces;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();
builder.Services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();
builder.Services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
builder.Services.AddScoped<IVehiculosPresentacion, VehiculosPresentacion>();
builder.Services.AddScoped<IVehiculoFotoPresentacion, VehiculoFotoPresentacion>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
