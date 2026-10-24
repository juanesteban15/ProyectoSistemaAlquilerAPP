using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class TarifasAplicacion : ITarifasAplicacion
    {
        private readonly IConexion conexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public TarifasAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Tarifas Actualizar(Tarifas entidad)
        {
            // Verifica que la tarifa solicitada exista antes de modificarla.
            var tarifaExistente = conexion.Tarifas!.FirstOrDefault(t => t.Id == entidad.Id);
            if (tarifaExistente == null)
                throw new InvalidOperationException($"No existe una tarifa con el Id {entidad.Id}.");

            // Comprueba que el vehículo asociado exista.
            if (!conexion.Vehiculos!.Any(v => v.Id == entidad.Vehiculo))
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Vehiculo}.");

            // El precio diario debe ser mayor que cero para poder calcular el costo de una reserva.
            if (entidad.PrecioPorDia <= 0)
                throw new InvalidOperationException("El precio por día debe ser mayor que cero.");

            // Actualiza la entidad rastreada y conserva las relaciones de Entity Framework.
            tarifaExistente.Vehiculo = entidad.Vehiculo;
            tarifaExistente.PrecioPorDia = entidad.PrecioPorDia;
            tarifaExistente.FechaInicio = entidad.FechaInicio;
            tarifaExistente.Activa = entidad.Activa;
            // El contexto usa consultas sin seguimiento; se marca explícitamente la entidad como modificada.
            conexion.Entry(tarifaExistente).State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó la tarifa con Id {entidad.Id}" });
            return tarifaExistente;
        }

        public Tarifas Borrar(Tarifas entidad)
        {
            // Recupera la tarifa y confirma que exista antes de eliminarla.
            var tarifaExistente = conexion.Tarifas!.FirstOrDefault(t => t.Id == entidad.Id);
            if (tarifaExistente == null)
                throw new InvalidOperationException($"No existe una tarifa con el Id {entidad.Id}.");

            // No permite eliminar una tarifa que ya está usada por reservas.
            if (conexion.Reservas!.Any(r => r.Tarifa == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar la tarifa con Id {entidad.Id} porque tiene reservas asociadas.");

            this.conexion.Tarifas!.Remove(tarifaExistente);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó la tarifa con Id {entidad.Id}" });
            return tarifaExistente;
        }

        public List<Tarifas> Consultar()
        {
            var tarifas = this.conexion.Tarifas!
                .Include(x => x._Vehiculo)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron las tarifas. Cantidad: {tarifas.Count}" });
            return tarifas;
        }

        public Tarifas Insertar(Tarifas entidad)
        {
            // Comprueba que el vehículo al que pertenece la tarifa exista.
            if (!conexion.Vehiculos!.Any(v => v.Id == entidad.Vehiculo))
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Vehiculo}.");

            // El precio diario debe ser mayor que cero para evitar tarifas inválidas.
            if (entidad.PrecioPorDia <= 0)
                throw new InvalidOperationException("El precio por día debe ser mayor que cero.");

            this.conexion.Tarifas!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó la tarifa con Id {entidad.Id}" });
            return entidad;
        }
    }
}
