using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IMarcasAplicacion
    {
        Marcas Insertar(Marcas entidad);
        List<Marcas> Consultar();
        Marcas Actualizar(Marcas entidad);
        Marcas Borrar(Marcas entidad);
    }
}
