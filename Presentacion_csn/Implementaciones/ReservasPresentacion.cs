using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class ReservasPresentacion : IReservasPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public ReservasPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public List<Reservas> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<Reservas>>("Reservas/Consultar", null!);
            task.Wait();
            return task.Result;
        }
    }
}
