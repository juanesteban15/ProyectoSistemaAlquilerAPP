using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Admin")]
    public class TarifasModel : PageModel
    {
        private readonly ITarifasPresentacion tarifasPresentacion;

        public TarifasModel(ITarifasPresentacion tarifasPresentacion)
        {
            this.tarifasPresentacion = tarifasPresentacion;
        }

        [BindProperty] public List<Tarifas> Lista { get; set; } = new();
        [BindProperty] public Tarifas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = tarifasPresentacion.Consultar();
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
            Actual = new Tarifas { FechaInicio = DateTime.Today, Activa = true };
            Formulario = true;
            Borrar = false;
        }

        public void OnPostBtEditar(int data)
        {
            try
            {
                Lista = tarifasPresentacion.Consultar();
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
                    throw new InvalidOperationException("No se recibieron los datos de la tarifa.");

                Actual = Actual.Id == 0
                    ? tarifasPresentacion.Insertar(Actual)
                    : tarifasPresentacion.Actualizar(Actual);

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
                Lista = tarifasPresentacion.Consultar();
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
                    throw new InvalidOperationException("No se recibió la tarifa que se quiere eliminar.");

                tarifasPresentacion.Borrar(Actual);
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
                Lista = tarifasPresentacion.Consultar();
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
