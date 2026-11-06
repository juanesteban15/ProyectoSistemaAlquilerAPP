using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Front_Alquiler.Pages.Cuenta
{
    public class IniciarSesionModel : PageModel
    {
        private readonly IUsuariosPresentacion usuariosPresentacion;

        public IniciarSesionModel(IUsuariosPresentacion usuariosPresentacion)
        {
            this.usuariosPresentacion = usuariosPresentacion;
        }

        [BindProperty]
        public LoginRequest Credenciales { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resultado = usuariosPresentacion.IniciarSesion(Credenciales);

            if (!resultado.Autenticado)
            {
                ModelState.AddModelError(string.Empty, "Usuario, correo o contraseña incorrectos.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, resultado.Id!.Value.ToString()),
                new Claim(ClaimTypes.Name, resultado.NombreUsuario ?? string.Empty)
            };

            if (!string.IsNullOrWhiteSpace(resultado.Rol))
            {
                claims.Add(new Claim(ClaimTypes.Role, resultado.Rol));
            }

            var identidad = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identidad));

            return RedirectToPage("/Index");
        }
    }
}