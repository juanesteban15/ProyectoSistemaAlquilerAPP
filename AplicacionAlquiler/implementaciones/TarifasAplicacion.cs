using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class TarifasAplicacion : ITarifasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public TarifasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Tarifas Actualizar(Tarifas entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Tarifas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó la tarifa con Id {entidad.Id}" });
            return entidad;
        }

        public Tarifas Borrar(Tarifas entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Tarifas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó la tarifa con Id {entidad.Id}" });
            return entidad;
        }

        public List<Tarifas> Consultar()
        {
            var tarifas = this.conexion.Tarifas!
                .Include(x => x._Vehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron las tarifas. Cantidad: {tarifas.Count}" });
            return tarifas;
        }

        public Tarifas Insertar(Tarifas entidad)
        {
            this.conexion.Tarifas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó la tarifa con Id {entidad.Id}" });
            return entidad;
        }
    }
}
