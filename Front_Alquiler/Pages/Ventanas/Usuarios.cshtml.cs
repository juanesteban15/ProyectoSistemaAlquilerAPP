using System.Net.Http.Json;
using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    public class UsuariosModel : PageModel
    {
        private readonly IUsuariosPresentacion usuariosPresentacion;

        public UsuariosModel(IUsuariosPresentacion usuariosPresentacion)
        {
            this.usuariosPresentacion = usuariosPresentacion;
            Filtro = new Usuarios();
        }

        [BindProperty] public List<Usuarios> Lista { get; set; } = new();
        [BindProperty] public Usuarios? Filtro { get; set; }
        [BindProperty] public Usuarios? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = usuariosPresentacion.Consultar();
                Formulario = false;
                Borrar = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtBuscar()
        {
            try
            {
                Filtro ??= new Usuarios();
                Filtro.NombreUsuario = Filtro.NombreUsuario?.Trim() ?? string.Empty;
                Lista = usuariosPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtNuevo()
        {
            Actual = new Usuarios();
            Formulario = true;
            Borrar = false;
        }

        public void OnPostBtEditar(int data)
        {
            try
            {
                Lista = usuariosPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(u => u.Id == data);
                Formulario = Actual != null;
                Borrar = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Actual == null)
                    throw new InvalidOperationException("No se recibieron los datos del usuario.");

                if (Actual.Id == 0)
                {
                    usuariosPresentacion.Insertar(Actual);
                }
                else
                {
                    // El servicio actual exige Contrasena al actualizar. Recupera el hash existente
                    // en el servidor para conservarlo, sin mostrarlo ni enviarlo como campo del formulario.
                    var usuarioExistente = usuariosPresentacion.Consultar()
                        .FirstOrDefault(u => u.Id == Actual.Id);

                    if (usuarioExistente == null)
                        throw new InvalidOperationException($"No existe el usuario con Id {Actual.Id}.");

                    Actual.Contrasena = usuarioExistente.Contrasena;
                    usuariosPresentacion.Actualizar(Actual);
                }

                Formulario = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                Formulario = true;
            }
            finally
            {
                try
                {
                    Lista = usuariosPresentacion.Consultar();
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        public void OnPostBtPregBorrar(int data)
        {
            try
            {
                Lista = usuariosPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(u => u.Id == data);
                Borrar = Actual != null;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtCancelarBorrar()
        {
            Borrar = false;
            Actual = null;
            OnPostBtRefrescar();
        }

        public async Task OnPostBtBorrar()
        {
            try
            {
                if (Actual == null || Actual.Id <= 0)
                    throw new InvalidOperationException("No se recibió el usuario que se quiere eliminar.");

                // El endpoint existente de Usuarios/Borrar usa HTTP DELETE.
                var usuarioCompleto = usuariosPresentacion.Consultar()
                    .FirstOrDefault(u => u.Id == Actual.Id);

                if (usuarioCompleto == null)
                    throw new InvalidOperationException($"No existe el usuario con Id {Actual.Id}.");

                using var cliente = new HttpClient();
                using var solicitud = new HttpRequestMessage(
                    HttpMethod.Delete,
                    "http://localhost:5182/Usuarios/Borrar")
                {
                    Content = JsonContent.Create(usuarioCompleto)
                };

                using var respuesta = await cliente.SendAsync(solicitud);
                if (!respuesta.IsSuccessStatusCode)
                {
                    var detalle = await respuesta.Content.ReadAsStringAsync();
                    throw new InvalidOperationException(
                        string.IsNullOrWhiteSpace(detalle)
                            ? $"La API respondió {(int)respuesta.StatusCode}."
                            : detalle);
                }

                Borrar = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                try
                {
                    Lista = usuariosPresentacion.Consultar();
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        private void MostrarError(Exception ex)
        {
            ViewData["Mensaje"] = ex.GetBaseException().Message;
        }
    }
}
