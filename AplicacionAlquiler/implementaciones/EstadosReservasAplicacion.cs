using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class EstadosReservasAplicacion : IEstadosReservasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public EstadosReservasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public EstadosReservas Actualizar(EstadosReservas entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<EstadosReservas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }

        public EstadosReservas Borrar(EstadosReservas entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.EstadosReservas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }

        public List<EstadosReservas> Consultar()
        {
            var resultados = this.conexion.EstadosReservas!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron estados de reservas. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public EstadosReservas Insertar(EstadosReservas entidad)
        {
            this.conexion.EstadosReservas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó estados de reservas con Id {entidad.Id}" });
            return entidad;
        }
    }
}
