using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using AplicacionAlquiler.Servicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Admin")]
    public class AuditoriaModel : PageModel
    {
        private readonly IRegistroAuditoriaPresentacion auditoriaPresentacion;

        public AuditoriaModel(IRegistroAuditoriaPresentacion auditoriaPresentacion)
        {
            this.auditoriaPresentacion = auditoriaPresentacion;
        }

        public List<RegistroAuditoria> Lista { get; set; } = new();

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = auditoriaPresentacion.Consultar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex.GetBaseException(), ViewData!);
            }
        }
    }
}
