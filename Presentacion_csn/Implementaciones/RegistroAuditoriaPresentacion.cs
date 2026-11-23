using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class RegistroAuditoriaPresentacion : IRegistroAuditoriaPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public RegistroAuditoriaPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public List<RegistroAuditoria> Consultar()
        {
            var task = comunicaciones.Ejecutar<List<RegistroAuditoria>>("RegistroAuditoria/Consultar", null!);
            task.Wait();
            return task.Result;
        }
    }
}
