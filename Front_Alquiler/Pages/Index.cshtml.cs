using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IVehiculosPresentacion _vehiculosPresentacion;

        public IndexModel(IVehiculosPresentacion vehiculosPresentacion)
        {
            _vehiculosPresentacion = vehiculosPresentacion;
        }

        public List<Vehiculos> Lista { get; private set; } = new();

        public string? Error { get; private set; }

        public void OnGet()
        {
            try
            {
                Lista = _vehiculosPresentacion.Consultar();
            }
            catch (Exception)
            {
                Lista = new List<Vehiculos>();
                Error = "No se pudieron cargar los vehículos.";
            }
        }
    }
}