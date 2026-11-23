using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

[Authorize(Roles = "Cliente")]
public class PublicarVehiculoModel : PageModel
{
    private readonly IVehiculosPresentacion vehiculosPresentacion;
    private readonly ICatalogosPresentacion catalogosPresentacion;

    public PublicarVehiculoModel(
        IVehiculosPresentacion vehiculosPresentacion,
        ICatalogosPresentacion catalogosPresentacion)
    {
        this.vehiculosPresentacion = vehiculosPresentacion;
        this.catalogosPresentacion = catalogosPresentacion;
    }

    [BindProperty]
    public CrearVehiculoClienteRequest Solicitud { get; set; } = new()
    {
        FechaInicioTarifa = DateTime.Today
    };

    [BindProperty]
    public IFormFile? FormFile { get; set; }

    public List<CatalogoItem> TiposVehiculos { get; set; } = new();
    public List<CatalogoItem> Marcas { get; set; } = new();
    public List<CatalogoItem> Combustibles { get; set; } = new();
    public List<CatalogoItem> Colores { get; set; } = new();
    public List<CatalogoItem> Transmisiones { get; set; } = new();
    public List<CatalogoItem> Categorias { get; set; } = new();

    public string? Mensaje { get; set; }

    private void CargarCatalogos()
    {
        TiposVehiculos = catalogosPresentacion.Consultar("TiposVehiculos");
        Marcas = catalogosPresentacion.Consultar("Marcas");
        Combustibles = catalogosPresentacion.Consultar("TiposCombustibles");
        Colores = catalogosPresentacion.Consultar("ColoresVehiculos");
        Transmisiones = catalogosPresentacion.Consultar("SistemasTransmisiones");
        Categorias = catalogosPresentacion.Consultar("Categorias");
    }

    public void OnGet()
    {
        TiposVehiculos = catalogosPresentacion.Consultar("TiposVehiculos");
        Marcas = catalogosPresentacion.Consultar("Marcas");
        Combustibles = catalogosPresentacion.Consultar("TiposCombustibles");
        Colores = catalogosPresentacion.Consultar("ColoresVehiculos");
        Transmisiones = catalogosPresentacion.Consultar("SistemasTransmisiones");
        Categorias = catalogosPresentacion.Consultar("Categorias");
    }
    public void OnPostGuardar()
    {
        try
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(claimId, out var propietarioId))
                throw new InvalidOperationException(
                    "No se pudo identificar al cliente conectado.");

            // El servidor asigna el propietario; no se toma del formulario.
            Solicitud.PropietarioId = propietarioId;

            if (FormFile is { Length: > 0 })
            {
                var memoryStream = new MemoryStream();
                FormFile.CopyToAsync(memoryStream).Wait();
                Solicitud.FotoBase64 = Convert.ToBase64String(memoryStream.ToArray());
                memoryStream.Dispose();
            }

            var resultado =
                vehiculosPresentacion.CrearParaCliente(Solicitud);

            Mensaje =
                $"Vehículo {resultado.VehiculoId} y tarifa {resultado.TarifaId} guardados.";
        }
        catch (Exception ex)
        {
            Mensaje = ex.GetBaseException().Message;
        }

        CargarCatalogos();
    }




}