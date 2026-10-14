using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class EstadosUsuariosAplicacion : IEstadosUsuariosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public EstadosUsuariosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public EstadosUsuarios Actualizar(EstadosUsuarios entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<EstadosUsuarios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó estados de usuarios con Id {entidad.Id}" });
            return entidad;
        }

        public EstadosUsuarios Borrar(EstadosUsuarios entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.EstadosUsuarios!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó estados de usuarios con Id {entidad.Id}" });
            return entidad;
        }

        public List<EstadosUsuarios> Consultar()
        {
            var resultados = this.conexion.EstadosUsuarios!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron estados de usuarios. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public EstadosUsuarios Insertar(EstadosUsuarios entidad)
        {
            this.conexion.EstadosUsuarios!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó estados de usuarios con Id {entidad.Id}" });
            return entidad;
        }
    }
}
