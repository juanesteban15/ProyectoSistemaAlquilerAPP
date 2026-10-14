using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IMetodosPagosAplicacion
    {
        MetodosPagos Insertar(MetodosPagos entidad);
        List<MetodosPagos> Consultar();
        MetodosPagos Actualizar(MetodosPagos entidad);
        MetodosPagos Borrar(MetodosPagos entidad);
    }
}
