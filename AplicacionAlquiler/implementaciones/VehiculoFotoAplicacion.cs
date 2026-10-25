using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class VehiculoFotoAplicacion : IVehiculoFotoAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public VehiculoFotoAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public VehiculoFoto Insertar(VehiculoFoto entidad)
        {
            Validar(entidad);
            conexion.VehiculoFotos!.Add(entidad);
            conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó la foto de vehículo con Id {entidad.Id}" });
            return entidad;
        }

        public List<VehiculoFoto> Consultar()
        {
            var fotos = conexion.VehiculoFotos!
                .Include(x => x._Vehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron las fotos de vehículos. Cantidad: {fotos.Count}" });
            return fotos;
        }

        public VehiculoFoto Actualizar(VehiculoFoto entidad)
        {
            var fotoExistente = conexion.VehiculoFotos!.FirstOrDefault(x => x.Id == entidad.Id);
            if (fotoExistente == null)
                throw new InvalidOperationException($"No existe una foto de vehículo con el Id {entidad.Id}.");

            Validar(entidad);
            fotoExistente.Vehiculo = entidad.Vehiculo;
            fotoExistente.Ruta = entidad.Ruta;
            conexion.Entry(fotoExistente).State = EntityState.Modified;
            conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó la foto de vehículo con Id {entidad.Id}" });
            return fotoExistente;
        }

        public VehiculoFoto Borrar(VehiculoFoto entidad)
        {
            var fotoExistente = conexion.VehiculoFotos!.FirstOrDefault(x => x.Id == entidad.Id);
            if (fotoExistente == null)
                throw new InvalidOperationException($"No existe una foto de vehículo con el Id {entidad.Id}.");

            conexion.VehiculoFotos.Remove(fotoExistente);
            conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó la foto de vehículo con Id {entidad.Id}" });
            return fotoExistente;
        }

        private void Validar(VehiculoFoto entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));
            if (!conexion.Vehiculos!.Any(x => x.Id == entidad.Vehiculo))
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Vehiculo}.");
            if (string.IsNullOrWhiteSpace(entidad.Ruta))
                throw new InvalidOperationException("La ruta de la foto es obligatoria.");
        }
    }
}
