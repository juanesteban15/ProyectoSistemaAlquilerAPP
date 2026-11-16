using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace Front_Alquiler.Pages.Ventanas
{
    public class UsuariosModel : PageModel
    {
        private readonly IUsuariosPresentacion IUsuariosPresentacion;

        public UsuariosModel(IUsuariosPresentacion IUsuariosPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
            Filtro = new Usuarios();
        }

        [BindProperty] public List<Usuarios> Lista { get; set; }
        [BindProperty] public Usuarios? Filtro { get; set; }
        [BindProperty] public Usuarios? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = this.IUsuariosPresentacion.Consultar();

            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostConsultar()
        {
            try
            {
                Filtro!.NombreUsuario = Filtro!.NombreUsuario?.Trim() ?? string.Empty;
                Lista = this.IUsuariosPresentacion.Buscar(Filtro!);
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
                Filtro!.NombreUsuario = Filtro!.NombreUsuario ?? "";
                Lista = this.IUsuariosPresentacion.Buscar(Filtro!);
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
                this.Actual = new Usuarios();
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
                if (this.Actual!.Id == 0)
                    this.Actual = this.IUsuariosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IUsuariosPresentacion.Actualizar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }


        }


        public void OnPostBtEditar(int data)
        {
            try
            {
                int id = Convert.ToInt32(data);
                Lista = IUsuariosPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(u => u.Id == id);
                Formulario = Actual != null;
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
                Lista = IUsuariosPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(u => u.Id == data);
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

        public async Task OnPostBtBorrar()
        {
            try
            {
                if (this.Actual == null || this.Actual.Id <= 0)
                {
                    throw new InvalidOperationException("No se recibió el vehículo que se quiere eliminar.");
                }

                this.IUsuariosPresentacion.Borrar(this.Actual);
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
