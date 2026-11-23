using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class DetalleVehiculoModel : PageModel
{
    private readonly IVehiculosPresentacion vehiculosPresentacion;
    private readonly ITarifasPresentacion tarifasPresentacion;

    public Vehiculos? Vehiculo { get; private set; }
    public Tarifas? Tarifa { get; private set; }

    public DetalleVehiculoModel(
        IVehiculosPresentacion vehiculosPresentacion,
        ITarifasPresentacion tarifasPresentacion)
    {
        this.vehiculosPresentacion = vehiculosPresentacion;
        this.tarifasPresentacion = tarifasPresentacion;
    }

    public IActionResult OnGet(int id)
    {
        Vehiculo = vehiculosPresentacion.Consultar()
            .FirstOrDefault(v => v.Id == id && v.EstadoVehiculo == 2);

        if (Vehiculo == null)
            return NotFound();

        Tarifa = tarifasPresentacion.Consultar()
            .FirstOrDefault(t => t.Vehiculo == id && t.Activa);

        return Page();
    }
}