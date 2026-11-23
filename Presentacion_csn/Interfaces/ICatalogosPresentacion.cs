using AplicacionAlquiler.Dominio.DTOs;

namespace AplicacionAlquiler.interfaces
{
    public interface ICatalogosPresentacion
    {
        List<CatalogoItem> Consultar(string tipo);
        CatalogoItem Insertar(CatalogoItem entidad);
        CatalogoItem Actualizar(CatalogoItem entidad);
        CatalogoItem Borrar(CatalogoItem entidad);
    }
}
