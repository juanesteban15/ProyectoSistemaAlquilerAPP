using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public ReservasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            // Verifica que la reserva exista antes de actualizarla.
            var reservaExistente = conexion.Reservas!.FirstOrDefault(r => r.Id == entidad.Id);
            if (reservaExistente == null)
                throw new InvalidOperationException($"No existe una reserva con el Id {entidad.Id}.");

            // Comprueba que la fecha final sea posterior a la fecha inicial.
            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio.");

            // Comprueba que cada registro referenciado exista antes de guardar la reserva.
            if (!conexion.Vehiculos!.Any(v => v.Id == entidad.Vehiculo))
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Vehiculo}.");
            if (!conexion.Usuarios!.Any(u => u.Id == entidad.Cliente))
                throw new InvalidOperationException($"No existe un usuario cliente con el Id {entidad.Cliente}.");
            if (!conexion.EstadosReservas!.Any(e => e.Id == entidad.EstadoReserva))
                throw new InvalidOperationException($"No existe un estado de reserva con el Id {entidad.EstadoReserva}.");
            if (!conexion.MetodosPagos!.Any(m => m.Id == entidad.MetodoPago))
                throw new InvalidOperationException($"No existe un método de pago con el Id {entidad.MetodoPago}.");
            var tarifa = conexion.Tarifas!.FirstOrDefault(t => t.Id == entidad.Tarifa);
            if (tarifa == null)
                throw new InvalidOperationException($"No existe una tarifa con el Id {entidad.Tarifa}.");
            if (tarifa.Vehiculo != entidad.Vehiculo)
                throw new InvalidOperationException("La tarifa seleccionada no pertenece al vehículo reservado.");
            if (!tarifa.Activa)
                throw new InvalidOperationException("No se puede reservar con una tarifa inactiva.");
            if (entidad.PrecioTotal < 0)
                throw new InvalidOperationException("El precio total no puede ser negativo.");

            // Actualiza solo los campos de la reserva, conservando la entidad rastreada.
            reservaExistente.Vehiculo = entidad.Vehiculo;
            reservaExistente.Cliente = entidad.Cliente;
            reservaExistente.EstadoReserva = entidad.EstadoReserva;
            reservaExistente.FechaInicio = entidad.FechaInicio;
            reservaExistente.FechaFin = entidad.FechaFin;
            reservaExistente.MetodoPago = entidad.MetodoPago;
            reservaExistente.Tarifa = entidad.Tarifa;
            reservaExistente.PrecioTotal = entidad.PrecioTotal;
            reservaExistente.Observaciones = entidad.Observaciones;
            // El contexto usa consultas sin seguimiento; se marca explícitamente la entidad como modificada.
            conexion.Entry(reservaExistente).State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó la reserva con Id {entidad.Id}" });
            return reservaExistente;
        }

        public Reservas Borrar(Reservas entidad)
        {
            // Recupera la reserva y confirma que exista antes de eliminarla.
            var reservaExistente = conexion.Reservas!.FirstOrDefault(r => r.Id == entidad.Id);
            if (reservaExistente == null)
                throw new InvalidOperationException($"No existe una reserva con el Id {entidad.Id}.");

            this.conexion.Reservas!.Remove(reservaExistente);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó la reserva con Id {entidad.Id}" });
            return reservaExistente;
        }

        public List<Reservas> Consultar()
        {
            var reservas = this.conexion.Reservas!
                .Include(x => x._Vehiculo)
                .Include(x => x._Cliente)
                .Include(x => x._EstadoReserva)
                .Include(x => x._MetodoPago)
                .Include(x => x._Tarifa)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron las reservas. Cantidad: {reservas.Count}" });
            return reservas;
        }

        public Reservas Insertar(Reservas entidad)
        {
            // Comprueba que el rango de fechas tenga sentido.
            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio.");

            // Comprueba que cada clave foránea apunte a un registro existente.
            if (!conexion.Vehiculos!.Any(v => v.Id == entidad.Vehiculo))
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Vehiculo}.");
            if (!conexion.Usuarios!.Any(u => u.Id == entidad.Cliente))
                throw new InvalidOperationException($"No existe un usuario cliente con el Id {entidad.Cliente}.");
            if (!conexion.EstadosReservas!.Any(e => e.Id == entidad.EstadoReserva))
                throw new InvalidOperationException($"No existe un estado de reserva con el Id {entidad.EstadoReserva}.");
            if (!conexion.MetodosPagos!.Any(m => m.Id == entidad.MetodoPago))
                throw new InvalidOperationException($"No existe un método de pago con el Id {entidad.MetodoPago}.");

            // Comprueba que la tarifa exista, esté activa y corresponda al vehículo reservado.
            var tarifa = conexion.Tarifas!.FirstOrDefault(t => t.Id == entidad.Tarifa);
            if (tarifa == null)
                throw new InvalidOperationException($"No existe una tarifa con el Id {entidad.Tarifa}.");
            if (tarifa.Vehiculo != entidad.Vehiculo)
                throw new InvalidOperationException("La tarifa seleccionada no pertenece al vehículo reservado.");
            if (!tarifa.Activa)
                throw new InvalidOperationException("No se puede reservar con una tarifa inactiva.");

            // Evita guardar montos negativos; el cálculo exacto depende de la regla de precios del proyecto.
            if (entidad.PrecioTotal < 0)
                throw new InvalidOperationException("El precio total no puede ser negativo.");

            this.conexion.Reservas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó la reserva con Id {entidad.Id}" });
            return entidad;
        }
    }
}
