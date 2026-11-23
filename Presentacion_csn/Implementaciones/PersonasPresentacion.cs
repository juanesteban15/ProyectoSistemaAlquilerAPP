using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class PersonasPresentacion : IPersonasPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public PersonasPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public Personas Insertar(Personas entidad)
        {
            if (entidad.Id != 0)
                throw new InvalidOperationException("La persona ya tiene Id.");
            return Ejecutar("Personas/Insertar", entidad);
        }

        public Personas Actualizar(Personas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("Selecciona una persona válida para actualizar.");
            return Ejecutar("Personas/Actualizar", entidad);
        }

        public List<Personas> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<Personas>>("Personas/Consultar", null!);
            task.Wait();
            return task.Result;
        }

        public Personas Borrar(Personas entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("Selecciona una persona válida para eliminar.");
            return Ejecutar("Personas/Borrar", entidad);
        }

        private Personas Ejecutar(string url, Personas entidad)
        {
            var task = comunicaciones.Ejecutar<Personas>(url, entidad);
            task.Wait();
            return task.Result;
        }
    }
}
