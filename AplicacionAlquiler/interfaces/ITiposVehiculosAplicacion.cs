using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface ITiposVehiculosAplicacion
    {
        TiposVehiculos Insertar(TiposVehiculos entidad);
        List<TiposVehiculos> Consultar();
        TiposVehiculos Actualizar(TiposVehiculos entidad);
        TiposVehiculos Borrar(TiposVehiculos entidad);
    }
}
