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
            // Verifica que la categoría exista antes de actualizarla.
            if (!conexion.Categorias!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe una categoría con el Id {entidad.Id}.");

            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");

            var entry = this.conexion!.Entry<Categorias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó categorías con Id {entidad.Id}" });
            return entidad;
        }

        public Categorias Borrar(Categorias entidad)
        {
            // Verifica que la categoría exista antes de eliminarla.
            if (!conexion.Categorias!.Any(x => x.Id == entidad.Id))
                throw new InvalidOperationException($"No existe una categoría con el Id {entidad.Id}.");
            // No permite eliminarla si hay vehículos que la utilizan.
            if (conexion.Vehiculos!.Any(x => x.Categoria == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar la categoría con Id {entidad.Id} porque tiene vehículos asociados.");

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
            // Comprueba que el tipo de vehículo asociado exista.
            if (!conexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");

            // Evita repetir el nombre de categoría para el mismo tipo de vehículo.
            if (conexion.Categorias!.Any(x => x.Nombre == entidad.Nombre && x.TipoVehiculo == entidad.TipoVehiculo))
                throw new InvalidOperationException($"Ya existe la categoría {entidad.Nombre} para ese tipo de vehículo.");

            this.conexion.Categorias!.Add(entidad);
            this.conexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó categorías con Id {entidad.Id}" });
            return entidad;
        }
    }
}
