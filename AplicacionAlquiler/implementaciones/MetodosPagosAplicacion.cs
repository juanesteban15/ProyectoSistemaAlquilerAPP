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
            // Verifica que el método de pago exista antes de actualizarlo.
            if (!conexion.MetodosPagos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un método de pago con el Id {entidad.Id}.");
            // Evita duplicar el nombre del método de pago.
            if (conexion.MetodosPagos!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el método de pago {entidad.Nombre}.");

            var entry = this.conexion!.Entry<MetodosPagos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó métodos de pago con Id {entidad.Id}" });
            return entidad;
        }

        public MetodosPagos Borrar(MetodosPagos entidad)
        {
            // Verifica que el método de pago exista antes de eliminarlo.
            if (!conexion.MetodosPagos!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe un método de pago con el Id {entidad.Id}.");
            // No permite eliminarlo si existen reservas que lo utilizan.
            if (conexion.Reservas!.Any(x => x.MetodoPago == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el método de pago con Id {entidad.Id} porque tiene reservas asociadas.");

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
            // Evita registrar dos veces el mismo método de pago.
            if (conexion.MetodosPagos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el método de pago {entidad.Nombre}.");
            // Evita registrar dos veces el mismo método de pago.
            if (conexion.MetodosPagos!.Any(x => x.Nombre == entidad.Nombre))
                throw new InvalidOperationException($"Ya existe el método de pago {entidad.Nombre}.");

            this.conexion.MetodosPagos!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó métodos de pago con Id {entidad.Id}" });
            return entidad;
        }
    }
}
