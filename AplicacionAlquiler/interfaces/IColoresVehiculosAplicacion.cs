using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IColoresVehiculosAplicacion
    {
        ColoresVehiculos Insertar(ColoresVehiculos entidad);
        List<ColoresVehiculos> Consultar();
        ColoresVehiculos Actualizar(ColoresVehiculos entidad);
        ColoresVehiculos Borrar(ColoresVehiculos entidad);
    }
}
