using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class TarifasPresentacion : ITarifasPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public TarifasPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public Tarifas Insertar(Tarifas entidad)
        {
            if (entidad.Id != 0)
                throw new InvalidOperationException("La tarifa ya tiene Id.");
            Validar(entidad);
            return Ejecutar("Tarifas/Insertar", entidad);
        }

        public Tarifas Actualizar(Tarifas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("Selecciona una tarifa válida para actualizar.");
            Validar(entidad);
            return Ejecutar("Tarifas/Actualizar", entidad);
        }

        public List<Tarifas> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<Tarifas>>("Tarifas/Consultar", null!);
            task.Wait();
            return task.Result;
        }

        public Tarifas Borrar(Tarifas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("Selecciona una tarifa válida para eliminar.");
            return Ejecutar("Tarifas/Borrar", entidad);
        }

        private Tarifas Ejecutar(string url, Tarifas entidad)
        {
            var task = comunicaciones.Ejecutar<Tarifas>(url, entidad);
            task.Wait();
            return task.Result;
        }

        private static void Validar(Tarifas entidad)
        {
            if (entidad.Vehiculo <= 0)
                throw new InvalidOperationException("El Id del vehículo debe ser mayor que cero.");
            if (entidad.PrecioPorDia <= 0)
                throw new InvalidOperationException("El precio por día debe ser mayor que cero.");
        }
    }
}
