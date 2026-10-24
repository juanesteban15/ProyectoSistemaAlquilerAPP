using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    public class VehiculosModel : PageModel
    {
        private readonly IVehiculosPresentacion vehiculosPresentacion;

        public VehiculosModel(IVehiculosPresentacion vehiculosPresentacion)
        {
            this.vehiculosPresentacion = vehiculosPresentacion;
        }

        [BindProperty] public List<Vehiculos> Lista { get; set; }

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = vehiculosPresentacion.Consultar();
            }
            catch (Exception ex)
            {
                Lista = new List<Vehiculos>();
            }
        }
    }
}
