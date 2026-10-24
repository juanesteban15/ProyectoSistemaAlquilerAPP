using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private readonly IReservasPresentacion reservasPresentacion;

        public ReservasModel(IReservasPresentacion reservasPresentacion)
        {
            this.reservasPresentacion = reservasPresentacion;
        }

        [BindProperty] public List<Reservas> Lista { get; set; }

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = reservasPresentacion.Consultar();
            }
            catch (Exception ex)
            {
                Lista = new List<Reservas>();
            }
        }
    }
}
