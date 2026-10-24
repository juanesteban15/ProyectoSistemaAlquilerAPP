using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class EstadosReservasAplicacion : IEstadosReservasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public EstadosReservasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public EstadosReservas Actualizar(EstadosReservas entidad)
        {
            // Verifica que el estado exista antes de actualizarlo.
            if (!conexion.EstadosReservas!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de reserva con el Id {entidad.Id}.");
            // Evita duplicar el nombre del estado en otro registro.
            if (conexion.EstadosReservas!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el estado de reserva {entidad.Nombre}.");

            var entry = this.conexion!.Entry<EstadosReservas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }

        public EstadosReservas Borrar(EstadosReservas entidad)
        {
            // Verifica que el estado exista antes de eliminarlo.
            if (!conexion.EstadosReservas!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de reserva con el Id {entidad.Id}.");
            // No permite eliminarlo si hay reservas que lo utilizan.
            if (conexion.Reservas!.Any(x => x.EstadoReserva == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el estado con Id {entidad.Id} porque tiene reservas asociadas.");

            this.conexion.EstadosReservas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }

        public List<EstadosReservas> Consultar()
        {
            var resultados = this.conexion.EstadosReservas!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron estados de reservas. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public EstadosReservas Insertar(EstadosReservas entidad)
        {
            // Evita registrar dos veces el mismo estado de reserva.
            if (conexion.EstadosReservas!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el estado de reserva {entidad.Nombre}.");

            this.conexion.EstadosReservas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }
    }
}
