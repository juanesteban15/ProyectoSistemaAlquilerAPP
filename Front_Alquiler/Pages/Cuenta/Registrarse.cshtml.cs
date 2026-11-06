using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Cuenta
{
    public class RegistrarseModel : PageModel
    {
        private readonly IUsuariosPresentacion usuariosPresentacion;

        public RegistrarseModel(IUsuariosPresentacion usuariosPresentacion)
        {
            this.usuariosPresentacion = usuariosPresentacion;
        }

        [BindProperty]
        public RegistroRequest Registro { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                var registrado = usuariosPresentacion.Registrar(Registro);

                if (registrado)
                {
                    return RedirectToPage("/Cuenta/IniciarSesion");
                }

                ModelState.AddModelError(
                    string.Empty,
                    "No se pudo completar el registro.");

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}