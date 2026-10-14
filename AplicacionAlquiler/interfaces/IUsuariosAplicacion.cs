using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Insertar(Usuarios entidad);
        List<Usuarios> Consultar();
        Usuarios Actualizar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
    }
}
