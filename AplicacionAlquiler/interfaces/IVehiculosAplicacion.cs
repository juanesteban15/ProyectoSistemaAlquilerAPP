using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IVehiculosAplicacion
    {
        Vehiculos Insertar(Vehiculos entidad);
        List<Vehiculos> Consultar();
        Vehiculos Actualizar(Vehiculos entidad);
        Vehiculos Borrar(Vehiculos entidad);
    }
}
