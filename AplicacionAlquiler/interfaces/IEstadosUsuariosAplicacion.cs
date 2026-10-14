using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IEstadosUsuariosAplicacion
    {
        EstadosUsuarios Insertar(EstadosUsuarios entidad);
        List<EstadosUsuarios> Consultar();
        EstadosUsuarios Actualizar(EstadosUsuarios entidad);
        EstadosUsuarios Borrar(EstadosUsuarios entidad);
    }
}
