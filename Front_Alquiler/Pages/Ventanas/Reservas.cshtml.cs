using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Admin")]
    public class ReservasModel : PageModel
    {
        private readonly IReservasPresentacion IReservasPresentacion;

        public ReservasModel(IReservasPresentacion reservasPresentacion)
        {
            this.IReservasPresentacion = reservasPresentacion;
            Filtro = new Reservas();
        }

        [BindProperty] public List<Reservas> Lista { get; set; } = new();
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = IReservasPresentacion.Consultar();
                Formulario = false;
                Borrar = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBuscar()
        {
            try
            {
                Filtro ??= new Reservas();
                Lista = IReservasPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                Borrar = false;
                Actual = new Reservas
                {
                    FechaInicio = DateTime.Now,
                    FechaFin = DateTime.Now,
                    FechaCreacion = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Actual == null)
                    throw new InvalidOperationException("No se recibieron los datos de la reserva.");

                if (Actual.Id == 0)
                    Actual = IReservasPresentacion.Insertar(Actual);
                else
                    Actual = IReservasPresentacion.Actualizar(Actual);

                Formulario = false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Formulario = true;
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {
                int id = Convert.ToInt32(data);
                Lista = IReservasPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(r => r.Id == id);
                Formulario = Actual != null;
                Borrar = false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(int data)
        {
            try
            {
                Lista = IReservasPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(r => r.Id == data);
                Borrar = Actual != null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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

                IReservasPresentacion.Borrar(Actual);
                Borrar = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtRefrescar();
            }
        }
    }
}
