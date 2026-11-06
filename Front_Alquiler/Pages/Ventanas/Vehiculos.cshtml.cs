using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    public class VehiculosModel : PageModel
    {
        private IVehiculosPresentacion IVehiculosPresentacion;

        public VehiculosModel(IVehiculosPresentacion IVehiculosPresentacion)
        {
            this.IVehiculosPresentacion = IVehiculosPresentacion;
            this.Filtro = new Vehiculos();
        }


        [BindProperty] public List<Vehiculos> Lista { get; set; }
        [BindProperty] public Vehiculos? Filtro { get; set; }
        [BindProperty] public Vehiculos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = this.IVehiculosPresentacion.Consultar();
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
                Filtro!.Placa = Filtro!.Placa ?? "";
                Lista = this.IVehiculosPresentacion.Buscar(Filtro!);
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
                Filtro!.Placa=Filtro!.Placa?? "";
                Lista = this.IVehiculosPresentacion.Buscar(Filtro!);
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
                this.Actual = new Vehiculos();
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
                    this.Actual = this.IVehiculosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IVehiculosPresentacion.Actualizar(this.Actual!);
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








        public void OnPostBtEditar(string data)
        {
            try
            {
                int id = Convert.ToInt32(data);
                Lista = IVehiculosPresentacion.Consultar();
                Actual = Lista.FirstOrDefault(v => v.Id == id);
                Formulario = Actual != null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }




        public void OnPostBtPregBorrar(int  data)
        {
            try
            {
                Lista = this.IVehiculosPresentacion.Consultar();
                Actual = this.Lista.FirstOrDefault(x => x.Id == data);
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
                if (this.Actual == null || this.Actual.Id <= 0)
                {
                    throw new InvalidOperationException("No se recibió el vehículo que se quiere eliminar.");
                }

                this.IVehiculosPresentacion.Borrar(this.Actual);
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
