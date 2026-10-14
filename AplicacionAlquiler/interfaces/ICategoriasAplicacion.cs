using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface ICategoriasAplicacion
    {
        Categorias Insertar(Categorias entidad);
        List<Categorias> Consultar();
        Categorias Actualizar(Categorias entidad);
        Categorias Borrar(Categorias entidad);
    }
}
