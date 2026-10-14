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
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Personas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();


            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "UPDATE",
                Detalle = $"Se actualizó la persona con Id {entidad.Id}"
            });


            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Personas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "DELETE",
                Detalle = $"Se eliminó la persona con Id {entidad.Id}"
            });
            return entidad;
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
            bool existe = conexion.Personas!.Any(h => h.NumeroDocumento == entidad.NumeroDocumento);
            if (existe)
            {
                throw new InvalidOperationException(
                    $"No existe una Persona con el Id {entidad.Id}.");
            }

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


