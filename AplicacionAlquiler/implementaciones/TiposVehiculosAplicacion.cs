using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class TiposVehiculosAplicacion : ITiposVehiculosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public TiposVehiculosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public TiposVehiculos Actualizar(TiposVehiculos entidad)
        {
            // Verifica que el tipo de vehículo exista antes de actualizarlo.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.Id}.");
            // Evita duplicar el nombre del tipo de vehículo.
            if (conexion.TiposVehiculos!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el tipo de vehículo {entidad.Nombre}.");

            var entry = this.conexion!.Entry<TiposVehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó tipos de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public TiposVehiculos Borrar(TiposVehiculos entidad)
        {
            // Verifica que el tipo de vehículo exista antes de eliminarlo.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.Id}.");
            // No permite eliminarlo mientras tenga vehículos o catálogos dependientes.
            if (conexion.Vehiculos!.Any(x => x.TipoVehiculo == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el tipo de vehículo con Id {entidad.Id} porque tiene vehículos asociados.");
            if (conexion.Marcas!.Any(x => x.TipoVehiculoId == entidad.Id) || conexion.Categorias!.Any(x => x.TipoVehiculo == entidad.Id) || conexion.SistemasTransmisiones!.Any(x => x.TipoVehiculo == entidad.Id) || conexion.TiposCombustibles!.Any(x => x.TipoVehiculo == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el tipo de vehículo con Id {entidad.Id} porque tiene catálogos asociados.");

            this.conexion.TiposVehiculos!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó tipos de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public List<TiposVehiculos> Consultar()
        {
            var resultados = this.conexion.TiposVehiculos!
                .Include(x => x.marcas)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron tipos de vehículos. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public TiposVehiculos Insertar(TiposVehiculos entidad)
        {
            // Evita registrar dos veces el mismo tipo de vehículo.
            if (conexion.TiposVehiculos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el tipo de vehículo {entidad.Nombre}.");
            // Evita registrar dos veces el mismo tipo de vehículo.
            if (conexion.TiposVehiculos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el tipo de vehículo {entidad.Nombre}.");

            this.conexion.TiposVehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó tipos de vehículos con Id {entidad.Id}" });
            return entidad;
        }
    }
}
