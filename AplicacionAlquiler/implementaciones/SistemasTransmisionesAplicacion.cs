using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class SistemasTransmisionesAplicacion : ISistemasTransmisionesAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public SistemasTransmisionesAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public SistemasTransmisiones Actualizar(SistemasTransmisiones entidad)
        {
            // Verifica que el sistema exista antes de actualizarlo.
            if (!conexion.SistemasTransmisiones!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un sistema de transmisión con el Id {entidad.Id}.");
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            // Evita duplicar el nombre para el mismo tipo de vehículo.
            if (conexion.SistemasTransmisiones!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el sistema de transmisión {entidad.Nombre} para ese tipo de vehículo.");

            var entry = this.conexion!.Entry<SistemasTransmisiones>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }

        public SistemasTransmisiones Borrar(SistemasTransmisiones entidad)
        {
            // Verifica que el sistema exista antes de eliminarlo.
            if (!conexion.SistemasTransmisiones!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un sistema de transmisión con el Id {entidad.Id}.");
            // No permite eliminarlo si algún vehículo lo está utilizando.
            if (conexion.Vehiculos!.Any(x => x.SistemaTransmision == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el sistema de transmisión con Id {entidad.Id} porque tiene vehículos asociados.");

            this.conexion.SistemasTransmisiones!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }

        public List<SistemasTransmisiones> Consultar()
        {
            var resultados = this.conexion.SistemasTransmisiones!
                .Include(x => x._TipoVehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron sistemas de transmisión. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public SistemasTransmisiones Insertar(SistemasTransmisiones entidad)
        {
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            // Evita repetir el sistema para el mismo tipo de vehículo.
            if (conexion.SistemasTransmisiones!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo))
                throw new InvalidOperationException($"Ya existe el sistema de transmisión {entidad.Nombre} para ese tipo de vehículo.");
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");

            // Evita repetir el sistema de transmisión para el mismo tipo de vehículo.
            if (conexion.SistemasTransmisiones!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo))
                throw new InvalidOperationException($"Ya existe el sistema de transmisión {entidad.Nombre} para ese tipo de vehículo.");

            this.conexion.SistemasTransmisiones!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }
    }
}
