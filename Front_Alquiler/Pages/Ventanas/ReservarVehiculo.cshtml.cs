using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Front_Alquiler.Pages.Ventanas
{
    [Authorize(Roles = "Cliente")]
    public class ReservarVehiculoModel : PageModel
    {
        private readonly IVehiculosPresentacion vehiculosPresentacion;
        private readonly ITarifasPresentacion tarifasPresentacion;
        private readonly IReservasPresentacion reservasPresentacion;
        private readonly ICatalogosPresentacion catalogosPresentacion;

        public ReservarVehiculoModel(
            IVehiculosPresentacion vehiculosPresentacion,
            ITarifasPresentacion tarifasPresentacion,
            IReservasPresentacion reservasPresentacion,
            ICatalogosPresentacion catalogosPresentacion)
        {
            this.vehiculosPresentacion = vehiculosPresentacion;
            this.tarifasPresentacion = tarifasPresentacion;
            this.reservasPresentacion = reservasPresentacion;
            this.catalogosPresentacion = catalogosPresentacion;
        }

        [BindProperty]
        public int VehiculoId { get; set; }

        [BindProperty]
        public DateTime? FechaInicio { get; set; }

        [BindProperty]
        public DateTime? FechaFin { get; set; }

        [BindProperty]
        public int? MetodoPagoId { get; set; }

        [BindProperty]
        public string? Observaciones { get; set; }

        public Vehiculos? Vehiculo { get; private set; }
        public Tarifas? Tarifa { get; private set; }
        public List<CatalogoItem> MetodosPago { get; private set; } = new();
        public string? Mensaje { get; private set; }

        public IActionResult OnGet(int vehiculoId)
        {
            VehiculoId = vehiculoId;

            if (!CargarDatos())
                return NotFound();

            FechaInicio = DateTime.Today;
            FechaFin = DateTime.Today.AddDays(1);

            return Page();
        }

        public IActionResult OnPostGuardar()
        {
            if (!CargarDatos())
                return NotFound();

            if (FechaInicio == null || FechaFin == null ||
                FechaFin.Value.Date <= FechaInicio.Value.Date)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "La fecha de fin debe ser posterior a la fecha de inicio.");
            }

            if (MetodoPagoId == null ||
                !MetodosPago.Any(m => m.Id == MetodoPagoId.Value))
            {
                ModelState.AddModelError(
                    nameof(MetodoPagoId),
                    "Selecciona un método de pago válido.");
            }

            if (!ModelState.IsValid)
                return Page();

            var clienteClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(clienteClaim, out int clienteId))
            {
                return Forbid();
            }

            var estadoPendiente = catalogosPresentacion
                .Consultar("EstadosReservas")
                .FirstOrDefault(e =>
                    e.Nombre.Equals("Pendiente", StringComparison.OrdinalIgnoreCase));

            if (estadoPendiente == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "No se encontró el estado Pendiente para crear la reserva.");

                return Page();
            }

            // Se cuenta cada día del intervalo.
            // Por ejemplo: del 10 al 11 equivale a un día de alquiler.
            int cantidadDias =
                (FechaFin!.Value.Date - FechaInicio!.Value.Date).Days;

            decimal precioTotal = cantidadDias * Tarifa!.PrecioPorDia;

            try
            {
                reservasPresentacion.Insertar(new Reservas
                {
                    Vehiculo = VehiculoId,
                    Cliente = clienteId,
                    EstadoReserva = estadoPendiente.Id,
                    FechaInicio = FechaInicio.Value.Date,
                    FechaFin = FechaFin.Value.Date,
                    MetodoPago = MetodoPagoId,
                    Tarifa = Tarifa.Id,
                    PrecioTotal = precioTotal,
                    Observaciones = Observaciones,
                    FechaCreacion = DateTime.Now
                });

                Mensaje = $"Solicitud de reserva enviada. Total: {precioTotal:C}.";
                ModelState.Clear();

                FechaInicio = DateTime.Today;
                FechaFin = DateTime.Today.AddDays(1);
                MetodoPagoId = null;
                Observaciones = null;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.GetBaseException().Message);
            }

            return Page();
        }

        private bool CargarDatos()
        {
            Vehiculo = vehiculosPresentacion.Consultar()
                .FirstOrDefault(v =>
                    v.Id == VehiculoId &&
                    v.EstadoVehiculo == 2);

            if (Vehiculo == null)
                return false;

            MetodosPago = catalogosPresentacion.Consultar("MetodosPagos");

            var fechaParaTarifa = FechaInicio?.Date ?? DateTime.Today;

            Tarifa = tarifasPresentacion.Consultar()
                .Where(t =>
                    t.Vehiculo == VehiculoId &&
                    t.Activa &&
                    t.FechaInicio.Date <= fechaParaTarifa)
                .OrderByDescending(t => t.FechaInicio)
                .FirstOrDefault();

            return Tarifa != null;
        }
    }
}