using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class VehiculosPresentacion : IVehiculosPresentacion
    {
        private readonly IComunicaciones IComunicaciones;

        public VehiculosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Vehiculos Borrar(Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Vehiculos>>(
                "Vehiculos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Vehiculos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Vehiculos>>(
                "Vehiculos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Vehiculos Insertar(Vehiculos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
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