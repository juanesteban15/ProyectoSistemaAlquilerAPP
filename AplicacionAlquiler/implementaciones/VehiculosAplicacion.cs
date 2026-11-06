using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class VehiculosAplicacion : IVehiculosAplicacion
    {
        private readonly IConexion IConexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        public VehiculosAplicacion(IConexion IConexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.IConexion = IConexion;
            this.auditoria = auditoria;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            // Verifica que el vehículo solicitado exista antes de actualizarlo.
            var vehiculoExistente = IConexion.Vehiculos!
                .FirstOrDefault(v => v.Id == entidad.Id);

            if (vehiculoExistente == null)
            {
                throw new InvalidOperationException(
                    $"No existe un vehículo con el Id {entidad.Id}.");
            }

            // Verifica que la placa no esté asignada a otro vehículo.
            var placaEnUso = IConexion.Vehiculos!
                .Any(v => v.Placa == entidad.Placa && v.Id != entidad.Id);

            if (placaEnUso)
            {
                throw new InvalidOperationException(
                    $"Ya existe otro vehículo con la placa {entidad.Placa}.");
            }

            // Comprueba que cada referencia opcional, si fue enviada, corresponda a un registro existente.
            if (entidad.Propietario.HasValue && !IConexion.Usuarios!.Any(x => x.Id == entidad.Propietario.Value))
                throw new InvalidOperationException($"No existe un usuario propietario con el Id {entidad.Propietario}.");
            if (entidad.EstadoVehiculo.HasValue && !IConexion.EstadosVehiculos!.Any(x => x.Id == entidad.EstadoVehiculo.Value))
                throw new InvalidOperationException($"No existe un estado de vehículo con el Id {entidad.EstadoVehiculo}.");
            if (entidad.TipoVehiculo.HasValue && !IConexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo.Value))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            if (entidad.Marca.HasValue && !IConexion.Marcas!.Any(x => x.Id == entidad.Marca.Value))
                throw new InvalidOperationException($"No existe una marca con el Id {entidad.Marca}.");
            if (entidad.ColorVehiculo.HasValue && !IConexion.ColoresVehiculos!.Any(x => x.Id == entidad.ColorVehiculo.Value))
                throw new InvalidOperationException($"No existe un color de vehículo con el Id {entidad.ColorVehiculo}.");
            if (entidad.SistemaTransmision.HasValue && !IConexion.SistemasTransmisiones!.Any(x => x.Id == entidad.SistemaTransmision.Value))
                throw new InvalidOperationException($"No existe un sistema de transmisión con el Id {entidad.SistemaTransmision}.");
            if (entidad.Categoria.HasValue && !IConexion.Categorias!.Any(x => x.Id == entidad.Categoria.Value))
                throw new InvalidOperationException($"No existe una categoría con el Id {entidad.Categoria}.");
            if (entidad.TipoCombustible.HasValue && !IConexion.TiposCombustibles!.Any(x => x.Id == entidad.TipoCombustible.Value))
                throw new InvalidOperationException($"No existe un tipo de combustible con el Id {entidad.TipoCombustible}.");

            // Aplica los cambios sobre la entidad rastreada para no marcar como modificados datos ajenos a la solicitud.
            vehiculoExistente.Placa = entidad.Placa;
            vehiculoExistente.Propietario = entidad.Propietario;
            vehiculoExistente.EstadoVehiculo = entidad.EstadoVehiculo;
            vehiculoExistente.TipoVehiculo = entidad.TipoVehiculo;
            vehiculoExistente.Marca = entidad.Marca;
            vehiculoExistente.ColorVehiculo = entidad.ColorVehiculo;
            vehiculoExistente.SistemaTransmision = entidad.SistemaTransmision;
            vehiculoExistente.Categoria = entidad.Categoria;
            vehiculoExistente.TipoCombustible = entidad.TipoCombustible;
            vehiculoExistente.Modelo = entidad.Modelo;
            // El contexto está configurado con consultas sin seguimiento; se marca la entidad para persistir los cambios.
            IConexion.Entry(vehiculoExistente).State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó el vehículo con Id {vehiculoExistente.Id}" });
            return vehiculoExistente;
        }

        public Vehiculos Borrar(Vehiculos entidad)
        {
            // Recupera el vehículo. La conexión usa consultas sin seguimiento.
            var vehiculoExistente = IConexion.Vehiculos!
                .FirstOrDefault(v => v.Id == entidad.Id);



            if (vehiculoExistente == null)
            {
                throw new InvalidOperationException($"No existe un vehículo con el Id {entidad.Id}.");
            }




            // Borrado lógico: conserva reservas y tarifas relacionadas.
            vehiculoExistente.Estado = false;
            vehiculoExistente.FechaEliminacion = DateTime.UtcNow;

            // Conexion está configurada con QueryTrackingBehavior.NoTracking.
            this.IConexion.Entry(vehiculoExistente).State = EntityState.Modified;
            this.IConexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se desactivó el vehículo con Id {vehiculoExistente.Id}" });
            return vehiculoExistente;
        }

        public List<Vehiculos> Consultar()
        {
            var vehiculos = this.IConexion.Vehiculos!
                .Where(v => v.Estado && v.FechaEliminacion == null)
                .Include(x => x._TipoVehiculo)
                .Include(x => x._ColorVehiculo)
                .Include(x => x._EstadoVehiculo)
                .Include(x => x._Marca)
                .Include(x => x._Propietario)
                .Include(x => x._SistemaTransmision)
                .Include(x => x._Categoria)
                .Include(x => x._TipoCombustible)
                .Include(x => x.VehiculoFoto)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron los vehículos. Cantidad: {vehiculos.Count}" });
            return vehiculos;
        }



        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            var consulta = this.IConexion.Vehiculos!
                .Where(v => v.Estado && v.FechaEliminacion == null);

            if (!string.IsNullOrWhiteSpace(entidad.Placa))
            {
                var placa = entidad.Placa.Trim();
                consulta = consulta.Where(v => v.Placa != null && v.Placa.Contains(placa));
            }

            return consulta
                .Take(5)
                .ToList();
        }







        public Vehiculos Insertar(Vehiculos entidad)
        {
            // Comprueba que la placa no esté registrada; la placa identifica al vehículo en este flujo.
            var vehiculoConMismaPlaca = IConexion.Vehiculos!
                .FirstOrDefault(v => v.Placa == entidad.Placa);

            if (vehiculoConMismaPlaca != null)
            {
                throw new InvalidOperationException("Ya existe un vehículo con esa placa.");
            }

            // Comprueba que el propietario indicado exista en la tabla de usuarios.
            if (entidad.Propietario.HasValue && !IConexion.Usuarios!.Any(x => x.Id == entidad.Propietario.Value))
                throw new InvalidOperationException($"No existe un usuario propietario con el Id {entidad.Propietario}.");
            if (entidad.EstadoVehiculo.HasValue && !IConexion.EstadosVehiculos!.Any(x => x.Id == entidad.EstadoVehiculo.Value))
                throw new InvalidOperationException($"No existe un estado de vehículo con el Id {entidad.EstadoVehiculo}.");
            if (entidad.TipoVehiculo.HasValue && !IConexion.TiposVehiculos!.Any(x => x.Id == entidad.TipoVehiculo.Value))
                throw new InvalidOperationException($"No existe un tipo de vehículo con el Id {entidad.TipoVehiculo}.");
            if (entidad.Marca.HasValue && !IConexion.Marcas!.Any(x => x.Id == entidad.Marca.Value))
                throw new InvalidOperationException($"No existe una marca con el Id {entidad.Marca}.");
            if (entidad.ColorVehiculo.HasValue && !IConexion.ColoresVehiculos!.Any(x => x.Id == entidad.ColorVehiculo.Value))
                throw new InvalidOperationException($"No existe un color de vehículo con el Id {entidad.ColorVehiculo}.");
            if (entidad.SistemaTransmision.HasValue && !IConexion.SistemasTransmisiones!.Any(x => x.Id == entidad.SistemaTransmision.Value))
                throw new InvalidOperationException($"No existe un sistema de transmisión con el Id {entidad.SistemaTransmision}.");
            if (entidad.Categoria.HasValue && !IConexion.Categorias!.Any(x => x.Id == entidad.Categoria.Value))
                throw new InvalidOperationException($"No existe una categoría con el Id {entidad.Categoria}.");
            if (entidad.TipoCombustible.HasValue && !IConexion.TiposCombustibles!.Any(x => x.Id == entidad.TipoCombustible.Value))
                throw new InvalidOperationException($"No existe un tipo de combustible con el Id {entidad.TipoCombustible}.");

            this.IConexion.Vehiculos!.Add(entidad);
            this.IConexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó el vehículo con Id {entidad.Id}" });
            return entidad;
        }


        private bool Validar(Vehiculos entidad)
        {
            return string.IsNullOrWhiteSpace(entidad.Placa)
                || !entidad.Propietario.HasValue
                || !entidad.EstadoVehiculo.HasValue
                || !entidad.TipoVehiculo.HasValue
                || !entidad.Marca.HasValue
                || !entidad.ColorVehiculo.HasValue
                || !entidad.SistemaTransmision.HasValue
                || !entidad.Categoria.HasValue
                || !entidad.TipoCombustible.HasValue
                || !entidad.Modelo.HasValue;
        }

    }
}
