using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IPersonasAplicacion
    {
        Personas Insertar(Personas entidad);
        List<Personas> Consultar();
        Personas Actualizar(Personas entidad);
        Personas Borrar(Personas entidad);
    }
}
