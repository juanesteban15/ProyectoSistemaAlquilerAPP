using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class MetodosPagosAplicacion : IMetodosPagosAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public MetodosPagosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public MetodosPagos Actualizar(MetodosPagos entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<MetodosPagos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó métodos de pago con Id {entidad.Id}" });
            return entidad;
        }

        public MetodosPagos Borrar(MetodosPagos entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.MetodosPagos!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó métodos de pago con Id {entidad.Id}" });
            return entidad;
        }

        public List<MetodosPagos> Consultar()
        {
            var resultados = this.conexion.MetodosPagos!
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron métodos de pago. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public MetodosPagos Insertar(MetodosPagos entidad)
        {
            this.conexion.MetodosPagos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó métodos de pago con Id {entidad.Id}" });
            return entidad;
        }
    }
}
