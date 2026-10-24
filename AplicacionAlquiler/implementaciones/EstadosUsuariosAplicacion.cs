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
            // Verifica que el estado exista antes de actualizarlo.
            if (!conexion.EstadosUsuarios!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de usuario con el Id {entidad.Id}.");
            // Evita duplicar el nombre del estado en otro registro.
            if (conexion.EstadosUsuarios!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el estado de usuario {entidad.Nombre}.");

            var entry = this.conexion!.Entry<EstadosUsuarios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó estados de usuarios con Id {entidad.Id}" });
            return entidad;
        }

        public EstadosUsuarios Borrar(EstadosUsuarios entidad)
        {
            // Verifica que el estado exista antes de eliminarlo.
            if (!conexion.EstadosUsuarios!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un estado de usuario con el Id {entidad.Id}.");
            // No permite eliminarlo si hay usuarios que lo utilizan.
            if (conexion.Usuarios!.Any(x => x.Estadousuario == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el estado con Id {entidad.Id} porque tiene usuarios asociados.");

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
            // Evita registrar dos veces el mismo estado de usuario.
            if (conexion.EstadosUsuarios!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el estado de usuario {entidad.Nombre}.");

            this.conexion.EstadosUsuarios!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó estados de usuarios con Id {entidad.Id}" });
            return entidad;
        }
    }
}
