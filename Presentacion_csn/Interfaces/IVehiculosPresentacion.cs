using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IVehiculosPresentacion
    {
        Vehiculos Insertar(Vehiculos entidad);
        Vehiculos Actualizar(Vehiculos entidad);
        List<Vehiculos> Consultar();
        List<Vehiculos> Buscar(Vehiculos entidad);
        Vehiculos Borrar(Vehiculos entidad);
    }
}
