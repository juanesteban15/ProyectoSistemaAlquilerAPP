using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IEstadosVehiculosAplicacion
    {
        EstadosVehiculos Insertar(EstadosVehiculos entidad);
        List<EstadosVehiculos> Consultar();
        EstadosVehiculos Actualizar(EstadosVehiculos entidad);
        EstadosVehiculos Borrar(EstadosVehiculos entidad);
    }
}
