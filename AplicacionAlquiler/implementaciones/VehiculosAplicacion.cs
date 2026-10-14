using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class VehiculosAplicacion : IVehiculosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public VehiculosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Vehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó el vehículo con Id {entidad.Id}" });
            return entidad;
        }

        public Vehiculos Borrar(Vehiculos entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Vehiculos!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó el vehículo con Id {entidad.Id}" });
            return entidad;
        }

        public List<Vehiculos> Consultar()
        {
            var vehiculos = this.conexion.Vehiculos!
                .Include(x => x._TipoVehiculo)
                .Include(x => x._ColorVehiculo)
                .Include(x => x._EstadoVehiculo)
                .Include(x => x._Marca)
                .Include(x => x._Propietario)
                .Include(x => x._SistemaTransmision)
                .Include(x => x._Categoria)
                .Include(x => x._TipoCombustible)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron los vehículos. Cantidad: {vehiculos.Count}" });
            return vehiculos;
        }

        public Vehiculos Insertar(Vehiculos entidad)
        {
            this.conexion.Vehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó el vehículo con Id {entidad.Id}" });
            return entidad;
        }
    }
}
