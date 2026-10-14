using AplicacionAlquiler.Servicio;
using AplicacionAlquiler.interfaces;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class RegistroAuditoriasAplicacion : IRegistroAuditoriaAplicacion
    {
        private readonly IConexion conexion;

        public RegistroAuditoriasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public RegistroAuditoria Insertar(RegistroAuditoria entidad)
        {
            conexion.RegistroAuditoria.Add(entidad);
            conexion.SaveChanges();

            return entidad;
        }

        public List<RegistroAuditoria> Consultar()
        {
            return conexion.RegistroAuditoria
                .AsNoTracking()
                .OrderByDescending(r => r.FechaUtc)
                .ToList();
        }
    }
}