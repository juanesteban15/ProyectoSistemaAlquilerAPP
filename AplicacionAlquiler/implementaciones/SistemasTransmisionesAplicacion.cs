using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class SistemasTransmisionesAplicacion : ISistemasTransmisionesAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public SistemasTransmisionesAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public SistemasTransmisiones Actualizar(SistemasTransmisiones entidad)
        {
            // Hacer calculo o operaciones

            var entry = this.conexion!.Entry<SistemasTransmisiones>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }

        public SistemasTransmisiones Borrar(SistemasTransmisiones entidad)
        {
            // Hacer calculo o operaciones

            this.conexion.SistemasTransmisiones!.Remove(entidad!);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }

        public List<SistemasTransmisiones> Consultar()
        {
            var resultados = this.conexion.SistemasTransmisiones!
                .Include(x => x._TipoVehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron sistemas de transmisión. Cantidad: {resultados.Count}" });
            return resultados;
        }

        public SistemasTransmisiones Insertar(SistemasTransmisiones entidad)
        {
            this.conexion.SistemasTransmisiones!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó sistemas de transmisión con Id {entidad.Id}" });
            return entidad;
        }
    }
}
