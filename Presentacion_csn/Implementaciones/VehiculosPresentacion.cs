using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class VehiculosPresentacion : IVehiculosPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public VehiculosPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public List<Vehiculos> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<Vehiculos>>("Vehiculos/Consultar", null!);
            task.Wait();
            return task.Result;
        }
    }
}
