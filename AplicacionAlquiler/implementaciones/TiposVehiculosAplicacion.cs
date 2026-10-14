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
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<TiposVehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó tipos de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public TiposVehiculos Borrar(TiposVehiculos entidad)
        {
            // Hacer calculo o operaciones

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
            this.conexion.TiposVehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó tipos de vehículos con Id {entidad.Id}" });
            return entidad;
        }
    }
}
