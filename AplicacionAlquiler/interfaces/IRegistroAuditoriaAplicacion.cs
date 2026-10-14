using AplicacionAlquiler.Servicio;


namespace AplicacionAlquiler.interfaces
{
    public interface IRegistroAuditoriaAplicacion
    {
        RegistroAuditoria Insertar(RegistroAuditoria entidad);
        List<RegistroAuditoria> Consultar();


    }
}


