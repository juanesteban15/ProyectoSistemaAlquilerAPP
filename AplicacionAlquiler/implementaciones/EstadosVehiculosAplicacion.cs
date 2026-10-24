using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class EstadosVehiculosAplicacion : IEstadosVehiculosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public EstadosVehiculosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public EstadosVehiculos Actualizar(EstadosVehiculos entidad)
        {
            // Verifica que el estado exista antes de actualizarlo.
            if (!conexion.EstadosVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de vehículo con el Id {entidad.Id}.");

            var entry = this.conexion!.Entry<EstadosVehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó estados de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public EstadosVehiculos Borrar(EstadosVehiculos entidad)
        {
            // Verifica que el estado exista antes de eliminarlo.
            if (!conexion.EstadosVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de vehículo con el Id {entidad.Id}.");
            // No permite eliminarlo si hay vehículos que lo utilizan.
            if (conexion.Vehiculos!.Any(x => x.EstadoVehiculo == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el estado con Id {entidad.Id} porque tiene vehículos asociados.");

            this.conexion.EstadosVehiculos!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó estados de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public List<EstadosVehiculos> Consultar()
        {
            var resultados = this.conexion.EstadosVehiculos!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron estados de vehículos. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public EstadosVehiculos Insertar(EstadosVehiculos entidad)
        {
            // Evita registrar dos veces el mismo estado de vehículo.
            if (conexion.EstadosVehiculos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el estado de vehículo {entidad.Nombre}.");

            this.conexion.EstadosVehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó estados de vehículos con Id {entidad.Id}" });
            return entidad;
        }
    }
}
