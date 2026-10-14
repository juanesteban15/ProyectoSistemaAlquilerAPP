using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class CategoriasAplicacion : ICategoriasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public CategoriasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Categorias Actualizar(Categorias entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<Categorias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó categorías con Id {entidad.Id}" });
            return entidad;
        }

        public Categorias Borrar(Categorias entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.Categorias!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó categorías con Id {entidad.Id}" });
            return entidad;
        }

        public List<Categorias> Consultar()
        {
            var resultados = this.conexion.Categorias!
                .Include(x => x._TipoVehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron categorías. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public Categorias Insertar(Categorias entidad)
        {
            this.conexion.Categorias!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó categorías con Id {entidad.Id}" });
            return entidad;
        }
    }
}
