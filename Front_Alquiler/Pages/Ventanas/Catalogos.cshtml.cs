using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Admin")]
    public class CatalogosModel : PageModel
    {
        private readonly ICatalogosPresentacion catalogosPresentacion;

        public CatalogosModel(ICatalogosPresentacion catalogosPresentacion)
        {
            this.catalogosPresentacion = catalogosPresentacion;
        }

        public IReadOnlyDictionary<string, string> Tipos { get; } =
            new Dictionary<string, string>
            {
                ["TiposVehiculos"] = "Tipos de vehículos",
                ["ColoresVehiculos"] = "Colores de vehículos",
                ["EstadosVehiculos"] = "Estados de vehículos",
                ["EstadosReservas"] = "Estados de reservas",
                ["EstadosUsuarios"] = "Estados de usuarios",
                ["Marcas"] = "Marcas",
                ["Categorias"] = "Categorías",
                ["TiposCombustibles"] = "Tipos de combustible",
                ["SistemasTransmisiones"] = "Sistemas de transmisión",
                ["MetodosPagos"] = "Métodos de pago"
            };

        [BindProperty] public string Tipo { get; set; } = "TiposVehiculos";
        [BindProperty] public List<CatalogoItem> Lista { get; set; } = new();
        [BindProperty] public CatalogoItem? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; }
        [BindProperty] public bool Borrar { get; set; }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                ValidarTipo();
                Lista = catalogosPresentacion.Consultar(Tipo);
                Formulario = false;
                Borrar = false;
                Actual = null;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                ValidarTipo();
                Actual = new CatalogoItem { Tipo = Tipo };
                Formulario = true;
                Borrar = false;
                Lista = catalogosPresentacion.Consultar(Tipo);
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
                ValidarTipo();
                if (Actual == null)
                    throw new InvalidOperationException("No se recibieron los datos del catálogo.");

                Actual.Tipo = Tipo;
                Actual = Actual.Id == 0
                    ? catalogosPresentacion.Insertar(Actual)
                    : catalogosPresentacion.Actualizar(Actual);

                Formulario = false;
                Borrar = false;
                Lista = catalogosPresentacion.Consultar(Tipo);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                Formulario = true;
                CargarLista();
            }
        }

        public void OnPostBtEditar(int data, string tipo)
        {
            try
            {
                Tipo = tipo;
                ValidarTipo();
                Lista = catalogosPresentacion.Consultar(Tipo);
                Actual = Lista.FirstOrDefault(x => x.Id == data);
                Formulario = Actual != null;
                Borrar = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        public void OnPostBtPregBorrar(int data, string tipo)
        {
            try
            {
                Tipo = tipo;
                ValidarTipo();
                Lista = catalogosPresentacion.Consultar(Tipo);
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
                ValidarTipo();
                if (Actual == null || Actual.Id <= 0)
                    throw new InvalidOperationException("No se recibió un registro válido para eliminar.");

                Actual.Tipo = Tipo;
                catalogosPresentacion.Borrar(Actual);
                Actual = null;
                Borrar = false;
                Lista = catalogosPresentacion.Consultar(Tipo);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
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

        private void ValidarTipo()
        {
            if (!Tipos.ContainsKey(Tipo))
                throw new InvalidOperationException("Selecciona un catálogo válido.");
        }

        private void CargarLista()
        {
            try
            {
                ValidarTipo();
                Lista = catalogosPresentacion.Consultar(Tipo);
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
