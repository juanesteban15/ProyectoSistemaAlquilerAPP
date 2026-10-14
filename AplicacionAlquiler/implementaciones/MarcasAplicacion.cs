using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class MarcasAplicacion : IMarcasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public MarcasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Marcas Actualizar(Marcas entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Marcas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó marcas con Id {entidad.Id}" });
            return entidad;
        }

        public Marcas Borrar(Marcas entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Marcas!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó marcas con Id {entidad.Id}" });
            return entidad;
        }

        public List<Marcas> Consultar()
        {
            var resultados = this.conexion.Marcas!
                .Include(x => x.TipoVehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron marcas. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public Marcas Insertar(Marcas entidad)
        {
            this.conexion.Marcas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó marcas con Id {entidad.Id}" });
            return entidad;
        }
    }
}
