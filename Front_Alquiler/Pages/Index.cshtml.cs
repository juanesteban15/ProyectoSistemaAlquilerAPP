using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages
{
    public class IndexModel : PageModel
    {
        private IVehiculosPresentacion IVehiculosPresentacion;

        public IndexModel(IVehiculosPresentacion IVehiculosPresentacion)
        {
            this.IVehiculosPresentacion = IVehiculosPresentacion;
            this.Filtro = new Vehiculos();
        }

        [BindProperty] public List<Vehiculos> Lista { get; set; }
        [BindProperty] public Vehiculos? Filtro { get; set; }
        [BindProperty] public Vehiculos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;


        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                var vehiculos = IVehiculosPresentacion.Consultar();

                 Lista = vehiculos
                .Where(v => v.EstadoVehiculo == 2)
                .ToList();

            }
            catch (Exception ex)
            {
                Lista = new List<Vehiculos>();
            }
        }
    }
}
