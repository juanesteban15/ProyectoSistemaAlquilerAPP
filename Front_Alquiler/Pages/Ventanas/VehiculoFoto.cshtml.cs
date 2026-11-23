using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Admin")]
    public class VehiculoFotoModel : PageModel
    {
        private readonly IVehiculoFotoPresentacion fotoPresentacion;

        public VehiculoFotoModel(IVehiculoFotoPresentacion fotoPresentacion)
        {
            this.fotoPresentacion = fotoPresentacion;
        }

        [BindProperty] public List<VehiculoFoto> Lista { get; set; } = new();
        [BindProperty] public VehiculoFoto? Actual { get; set; }
        [BindProperty] public IFormFile? FormFile { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = fotoPresentacion.Consultar();
                Actual = null;
                Formulario = false;
                Borrar = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtNuevo()
        {
            Actual = new VehiculoFoto();
            Formulario = true;
            Borrar = false;
        }

        public void OnPostBtEditar(int data)
        {
            try
            {
                Lista = fotoPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(x => x.Id == data);
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
                    throw new InvalidOperationException("No se recibieron los datos de la foto.");

                if (Actual.Vehiculo is null or <= 0)
                    throw new InvalidOperationException("Selecciona un Id de vehículo válido.");

                if (FormFile is { Length: > 0 })
                {
                    var memoryStream = new MemoryStream();
                    FormFile.CopyToAsync(memoryStream).Wait();
                    Actual.Ruta = Convert.ToBase64String(memoryStream.ToArray());
                    memoryStream.Dispose();
                }

                if (string.IsNullOrWhiteSpace(Actual.Ruta))
                    throw new InvalidOperationException("Selecciona una imagen para guardar.");

                Actual = Actual.Id == 0
                    ? fotoPresentacion.Insertar(Actual)
                    : fotoPresentacion.Actualizar(Actual);

                Formulario = false;
                Borrar = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                Formulario = true;
            }
            finally
            {
                CargarLista();
            }
        }

        public void OnPostBtPregBorrar(int data)
        {
            try
            {
                Lista = fotoPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(x => x.Id == data);
                Borrar = Actual != null;
                Formulario = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Actual == null || Actual.Id <= 0)
                    throw new InvalidOperationException("No se recibió la foto que se quiere eliminar.");

                fotoPresentacion.Borrar(Actual);
                Actual = null;
                Borrar = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                CargarLista();
            }
        }

        public void OnPostBtCancelar()
        {
            Actual = null;
            Formulario = false;
            Borrar = false;
            OnPostBtRefrescar();
        }

        private void CargarLista()
        {
            try
            {
                Lista = fotoPresentacion.Consultar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarError(Exception ex)
        {
            LogConversor.Log(ex.GetBaseException(), ViewData!);
        }
    }
}
