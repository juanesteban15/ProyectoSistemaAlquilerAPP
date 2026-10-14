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
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<ColoresVehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó colores de vehículos con Id {entidad.Id}" });
            return entidad;
        }

        public ColoresVehiculos Borrar(ColoresVehiculos entidad)
        {
            // Hacer calculo o operaciones

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
            this.conexion.ColoresVehiculos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó colores de vehículos con Id {entidad.Id}" });
            return entidad;
        }
    }
}
