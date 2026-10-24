using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class TiposCombustiblesAplicacion : ITiposCombustiblesAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public TiposCombustiblesAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public TiposCombustibles Actualizar(TiposCombustibles entidad)
        {
            // Verifica que el tipo de combustible exista antes de actualizarlo.
            if (!conexion.TiposCombustibles!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un tipo de combustible con el Id {entidad.Id}.");
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            // Evita duplicar el nombre para el mismo tipo de vehículo.
            if (conexion.TiposCombustibles!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el combustible {entidad.Nombre} para ese tipo de vehículo.");

            var entry = this.conexion!.Entry<TiposCombustibles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }

        public TiposCombustibles Borrar(TiposCombustibles entidad)
        {
            // Verifica que el tipo de combustible exista antes de eliminarlo.
            if (!conexion.TiposCombustibles!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un tipo de combustible con el Id {entidad.Id}.");
            // No permite eliminarlo si algún vehículo lo está utilizando.
            if (conexion.Vehiculos!.Any(x => x.TipoCombustible == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el combustible con Id {entidad.Id} porque tiene vehículos asociados.");

            this.conexion.TiposCombustibles!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }

        public List<TiposCombustibles> Consultar()
        {
            var resultados = this.conexion.TiposCombustibles!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron tipos de combustible. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public TiposCombustibles Insertar(TiposCombustibles entidad)
        {
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            // Evita repetir el combustible para el mismo tipo de vehículo.
            if (conexion.TiposCombustibles!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo))
                throw new InvalidOperationException($"Ya existe el combustible {entidad.Nombre} para ese tipo de vehículo.");
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");

            // Evita repetir el tipo de combustible para el mismo tipo de vehículo.
            if (conexion.TiposCombustibles!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo))
                throw new InvalidOperationException($"Ya existe el combustible {entidad.Nombre} para ese tipo de vehículo.");

            this.conexion.TiposCombustibles!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }
    }
}
