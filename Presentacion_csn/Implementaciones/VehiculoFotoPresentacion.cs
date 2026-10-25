using AplicacionAlquiler.Dominio.Entidades;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class VehiculoFotoPresentacion : IVehiculoFotoPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public VehiculoFotoPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public VehiculoFoto Insertar(VehiculoFoto entidad)
        {
            Validar(entidad);
            if (entidad.Id != 0)
                throw new Exception("La foto ya está guardada.");

            var task = comunicaciones.Ejecutar<VehiculoFoto>("VehiculoFoto/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public VehiculoFoto Actualizar(VehiculoFoto entidad)
        {
            Validar(entidad);
            if (entidad.Id == 0)
                throw new Exception("La foto no se ha guardado.");

            var task = comunicaciones.Ejecutar<VehiculoFoto>("VehiculoFoto/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<VehiculoFoto> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<VehiculoFoto>>("VehiculoFoto/Consultar", null);
            task.Wait();
            return task.Result;
        }

        public VehiculoFoto Borrar(VehiculoFoto entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("Selecciona una foto guardada para eliminar.");

            var task = comunicaciones.Ejecutar<VehiculoFoto>("VehiculoFoto/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        private static void Validar(VehiculoFoto entidad)
        {
            if (entidad == null || entidad.Vehiculo <= 0 || string.IsNullOrWhiteSpace(entidad.Ruta))
                throw new Exception("Falta el vehículo o la ruta de la foto.");
        }
    }
}
