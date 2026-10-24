using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public UsuariosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            // Verifica que el usuario que se desea actualizar exista.
            var usuarioExistente = conexion.Usuarios!.FirstOrDefault(u => u.Id == entidad.Id);
            if (usuarioExistente == null)
                throw new InvalidOperationException($"No existe un usuario con el Id {entidad.Id}.");

            // Comprueba que la persona asociada exista cuando se indicó una persona.
            if (entidad.Persona.HasValue && !conexion.Personas!.Any(p => p.Id == entidad.Persona.Value))
                throw new InvalidOperationException($"No existe una persona con el Id {entidad.Persona}.");

            // Evita que el nombre de usuario se repita en otra cuenta.
            if (conexion.Usuarios!.Any(u => u.NombreUsuario == entidad.NombreUsuario && u.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el nombre de usuario {entidad.NombreUsuario}.");

            usuarioExistente.Persona = entidad.Persona;
            usuarioExistente.NombreUsuario = entidad.NombreUsuario;
            usuarioExistente.Contrasena = entidad.Contrasena;
            usuarioExistente.Rol = entidad.Rol;
            usuarioExistente.Estadousuario = entidad.Estadousuario;
            usuarioExistente.FechaEliminacion = entidad.FechaEliminacion;
            // El contexto usa consultas sin seguimiento; se marca explícitamente la entidad como modificada.
            conexion.Entry(usuarioExistente).State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó el usuario con Id {entidad.Id}" });
            return usuarioExistente;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            // Recupera el usuario y confirma que exista antes de eliminarlo.
            var usuarioExistente = conexion.Usuarios!.FirstOrDefault(u => u.Id == entidad.Id);
            if (usuarioExistente == null)
                throw new InvalidOperationException($"No existe un usuario con el Id {entidad.Id}.");

            // No permite eliminar al usuario si tiene vehículos o reservas relacionados.
            if (conexion.Vehiculos!.Any(v => v.Propietario == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el usuario con Id {entidad.Id} porque es propietario de vehículos.");
            if (conexion.Reservas!.Any(r => r.Cliente == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el usuario con Id {entidad.Id} porque tiene reservas asociadas.");

            this.conexion.Usuarios!.Remove(usuarioExistente);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó el usuario con Id {entidad.Id}" });
            return usuarioExistente;
        }

        public List<Usuarios> Consultar()
        {
            var usuarios = this.conexion.Usuarios!
                .Include(x => x._Persona).Include(x => x._EstadoUsuario)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron los usuarios. Cantidad: {usuarios.Count}" });
            return usuarios;
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            var ExistePersona = this.conexion.Personas!
                .FirstOrDefault(h => h.Id == entidad.Persona);

            if (ExistePersona == null)
            {
                throw new InvalidOperationException(
                    $"No existe una persona con el Id {entidad.Persona}.");
            }

            // Evita crear otra cuenta con el mismo nombre de usuario.
            if (this.conexion.Usuarios!.Any(u => u.NombreUsuario == entidad.NombreUsuario))
            {
                throw new InvalidOperationException($"Ya existe el nombre de usuario {entidad.NombreUsuario}.");
            }

            this.conexion.Usuarios!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó el usuario con Id {entidad.Id}" });

            return entidad;
        }
    }
}
