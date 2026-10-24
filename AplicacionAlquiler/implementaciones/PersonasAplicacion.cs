using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class PersonasAplicacion : IPersonasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public PersonasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria; 
        }

        public Personas Actualizar(Personas entidad)
        {
            // Verifica que la persona que se desea actualizar exista.
            var personaExistente = conexion.Personas!.FirstOrDefault(p => p.Id == entidad.Id);
            if (personaExistente == null)
                throw new InvalidOperationException($"No existe una persona con el Id {entidad.Id}.");

            // Evita duplicar el número de documento en otra persona.
            if (conexion.Personas!.Any(p => p.NumeroDocumento == entidad.NumeroDocumento && p.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe una persona con el número de documento {entidad.NumeroDocumento}.");

            personaExistente.Nombre = entidad.Nombre;
            personaExistente.Apellido = entidad.Apellido;
            personaExistente.Email = entidad.Email;
            personaExistente.PaisNacimiento = entidad.PaisNacimiento;
            personaExistente.FechaNacimiento = entidad.FechaNacimiento;
            personaExistente.TipoDocumento = entidad.TipoDocumento;
            personaExistente.NumeroDocumento = entidad.NumeroDocumento;
            personaExistente.Genero = entidad.Genero;
            personaExistente.ComplementoPais = entidad.ComplementoPais;
            personaExistente.Telefono = entidad.Telefono;
            // El contexto usa consultas sin seguimiento; se marca explícitamente la entidad como modificada.
            conexion.Entry(personaExistente).State = EntityState.Modified;
            this.conexion!.SaveChanges();


            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "UPDATE",
                Detalle = $"Se actualizó la persona con Id {entidad.Id}"
            });


            return personaExistente;
        }

        public Personas Borrar(Personas entidad)
        {
            // Recupera la persona y confirma que exista antes de eliminarla.
            var personaExistente = conexion.Personas!.FirstOrDefault(p => p.Id == entidad.Id);
            if (personaExistente == null)
                throw new InvalidOperationException($"No existe una persona con el Id {entidad.Id}.");

            // No permite eliminar una persona que todavía tiene una cuenta de usuario asociada.
            if (conexion.Usuarios!.Any(u => u.Persona == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar la persona con Id {entidad.Id} porque tiene usuarios asociados.");

            this.conexion.Personas!.Remove(personaExistente);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "DELETE",
                Detalle = $"Se eliminó la persona con Id {entidad.Id}"
            });
            return personaExistente;
        }

        public List<Personas> Consultar()
        {
            var personas = conexion.Personas!.ToList();
            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "CONSULTA",
                Detalle = $"Se consultaron las personas. Cantidad: {personas.Count}"
            });
            return personas;

        }

        public Personas Insertar(Personas entidad)
        {
            // Evita registrar dos personas con el mismo número de documento.
            if (conexion.Personas!.Any(p => p.NumeroDocumento == entidad.NumeroDocumento))
                throw new InvalidOperationException($"Ya existe una persona con el número de documento {entidad.NumeroDocumento}.");

            // Evita registrar dos personas con el mismo correo cuando se proporciona uno.
            if (!string.IsNullOrWhiteSpace(entidad.Email) && conexion.Personas!.Any(p => p.Email == entidad.Email))
                throw new InvalidOperationException($"Ya existe una persona con el correo {entidad.Email}.");

            this.conexion.Personas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "INSERT",
                Detalle = $"Se insertó la persona con Id {entidad.Id}"
            });
            return entidad;
        }
    }
}


