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
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó la reserva con Id {entidad.Id}" });
            return entidad;
        }

        public Reservas Borrar(Reservas entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Reservas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó la reserva con Id {entidad.Id}" });
            return entidad;
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
            this.conexion.Reservas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó la reserva con Id {entidad.Id}" });
            return entidad;
        }
    }
}
