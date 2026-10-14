using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class TiposCombustiblesAplicacion : ITiposCombustiblesAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public TiposCombustiblesAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public TiposCombustibles Actualizar(TiposCombustibles entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<TiposCombustibles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }

        public TiposCombustibles Borrar(TiposCombustibles entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.TiposCombustibles!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }

        public List<TiposCombustibles> Consultar()
        {
            var resultados = this.conexion.TiposCombustibles!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron tipos de combustible. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public TiposCombustibles Insertar(TiposCombustibles entidad)
        {
            this.conexion.TiposCombustibles!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó tipos de combustible con Id {entidad.Id}" });
            return entidad;
        }
    }
}
