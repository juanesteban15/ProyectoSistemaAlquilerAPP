using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface ITiposCombustiblesAplicacion
    {
        TiposCombustibles Insertar(TiposCombustibles entidad);
        List<TiposCombustibles> Consultar();
        TiposCombustibles Actualizar(TiposCombustibles entidad);
        TiposCombustibles Borrar(TiposCombustibles entidad);
    }
}
