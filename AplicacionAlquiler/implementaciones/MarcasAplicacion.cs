using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class MarcasAplicacion : IMarcasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public MarcasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Marcas Actualizar(Marcas entidad)
        {
            // Verifica que la marca exista antes de actualizarla.
            if (!conexion.Marcas!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe una marca con el Id {entidad.Id}.");
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculoId))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculoId}.");
            // Evita duplicar el nombre de marca en el mismo tipo de vehículo.
            if (conexion.Marcas!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculoId == entidad.TipoVehiculoId && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe la marca {entidad.Nombre} para ese tipo de vehículo.");

            var entry = this.conexion!.Entry<Marcas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó marcas con Id {entidad.Id}" });
            return entidad;
        }

        public Marcas Borrar(Marcas entidad)
        {
            // Verifica que la marca exista antes de eliminarla.
            if (!conexion.Marcas!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe una marca con el Id {entidad.Id}.");
            // No permite eliminarla si algún vehículo la utiliza.
            if (conexion.Vehiculos!.Any(x => x.Marca == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar la marca con Id {entidad.Id} porque tiene vehículos asociados.");

            this.conexion.Marcas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó marcas con Id {entidad.Id}" });
            return entidad;
        }

        public List<Marcas> Consultar()
        {
            var resultados = this.conexion.Marcas!
                .Include(x => x.TipoVehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron marcas. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public Marcas Insertar(Marcas entidad)
        {
            // Comprueba que el tipo de vehículo asociado a la marca exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculoId))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculoId}.");
            // Evita duplicar una marca dentro del mismo tipo de vehículo.
            if (conexion.Marcas!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculoId == entidad.TipoVehiculoId))
                throw new InvalidOperationException($"Ya existe la marca {entidad.Nombre} para ese tipo de vehículo.");
            // Comprueba que el tipo de vehículo asociado a la marca exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculoId))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculoId}.");

            // Evita duplicar una marca dentro del mismo tipo de vehículo.
            if (conexion.Marcas!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculoId == entidad.TipoVehiculoId))
                throw new InvalidOperationException($"Ya existe la marca {entidad.Nombre} para ese tipo de vehículo.");

            this.conexion.Marcas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó marcas con Id {entidad.Id}" });
            return entidad;
        }
    }
}
