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
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó el usuario con Id {entidad.Id}" });
            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Usuarios!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó el usuario con Id {entidad.Id}" });
            return entidad;
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
                    $"No existe una habitación con el Id {entidad.Persona}.");
            }

            // La habitación ya existe en la base de datos: no debe insertarse otra vez.

            this.conexion.Usuarios!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó el usuario con Id {entidad.Id}" });

            return entidad;
        }
    }
}
