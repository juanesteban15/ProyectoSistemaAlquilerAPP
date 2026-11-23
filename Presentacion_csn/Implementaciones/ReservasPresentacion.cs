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

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad.Id != 0)
                throw new InvalidOperationException("La reserva ya fue guardada.");

            var task = comunicaciones.Ejecutar<Reservas>("Reservas/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("No se recibió una reserva válida para actualizar.");

            var task = comunicaciones.Ejecutar<Reservas>("Reservas/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("No se recibió una reserva válida para eliminar.");

            var task = comunicaciones.Ejecutar<Reservas>("Reservas/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<Reservas> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<Reservas>>("Reservas/Consultar", null!);
            task.Wait();
            return task.Result;
        }

        public List<Reservas> Buscar(Reservas entidad)
        {
            var task = comunicaciones.Ejecutar<List<Reservas>>("Reservas/Buscar", entidad);
            task.Wait();
            return task.Result;
        }
    }
}
