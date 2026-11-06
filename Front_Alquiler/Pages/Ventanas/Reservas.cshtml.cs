using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentacion_CSN.Interfaces;

namespace Front_Alquiler.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private readonly IReservasPresentacion reservasPresentacion;
        private readonly IComunicaciones comunicaciones;

        public ReservasModel(
            IReservasPresentacion reservasPresentacion,
            IComunicaciones comunicaciones)
        {
            this.reservasPresentacion = reservasPresentacion;
            this.comunicaciones = comunicaciones;
            Filtro = new Reservas();
        }

        [BindProperty] public List<Reservas> Lista { get; set; } = new();
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = reservasPresentacion.Consultar();
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
                Filtro ??= new Reservas();
                Lista = reservasPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtNuevo()
        {
            Actual = new Reservas
            {
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                FechaCreacion = DateTime.Now
            };
            Formulario = true;
            Borrar = false;
        }

        public void OnPostBtEditar(int data)
        {
            try
            {
                Lista = reservasPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(r => r.Id == data);
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
                    throw new InvalidOperationException("No se recibieron los datos de la reserva.");

                if (Actual.Id == 0)
                    EjecutarApi<Reservas>("Reservas/Insertar", Actual);
                else
                    EjecutarApi<Reservas>("Reservas/Actualizar", Actual);

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
                    Lista = reservasPresentacion.Consultar();
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
                Lista = reservasPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(r => r.Id == data);
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

        public void OnPostBtBorrar()
        {
            try
            {
                if (Actual == null || Actual.Id <= 0)
                    throw new InvalidOperationException("No se recibió la reserva que se quiere eliminar.");

                EjecutarApi<Reservas>("Reservas/Borrar", Actual);
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
                    Lista = reservasPresentacion.Consultar();
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        private T EjecutarApi<T>(string url, object entidad)
        {
            var task = comunicaciones.Ejecutar<T>(url, entidad);
            task.Wait();
            return task.Result;
        }

        private void MostrarError(Exception ex)
        {
            ViewData["Mensaje"] = ex.GetBaseException().Message;
        }
    }
}
