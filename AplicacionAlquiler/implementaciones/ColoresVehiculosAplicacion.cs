using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class ColoresVehiculosAplicacion : IColoresVehiculosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public ColoresVehiculosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public ColoresVehiculos Actualizar(ColoresVehiculos entidad)
        {
            // Verifica que el color exista antes de actualizarlo.
            if (!conexion.ColoresVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un color de vehículo con el Id {entidad.Id}.");
            // Evita asignar a otro registro un nombre de color que ya está en uso.
            if (conexion.ColoresVehiculos!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el color {entidad.Nombre}.");

            var entry = this.conexion!.Entry<ColoresVehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó colores de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public ColoresVehiculos Borrar(ColoresVehiculos entidad)
        {
            // Verifica que el color exista antes de eliminarlo.
            if (!conexion.ColoresVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un color de vehículo con el Id {entidad.Id}.");
            // No permite eliminarlo si algún vehículo lo está utilizando.
            if (conexion.Vehiculos!.Any(x => x.ColorVehiculo == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el color con Id {entidad.Id} porque tiene vehículos asociados.");

            this.conexion.ColoresVehiculos!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó colores de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public List<ColoresVehiculos> Consultar()
        {
            var resultados = this.conexion.ColoresVehiculos!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron colores de vehículos. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public ColoresVehiculos Insertar(ColoresVehiculos entidad)
        {
            // Evita registrar dos veces el mismo color.
            if (conexion.ColoresVehiculos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el color {entidad.Nombre}.");

            this.conexion.ColoresVehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó colores de vehículos con Id {entidad.Id}" });
            return entidad;
        }
    }
}
