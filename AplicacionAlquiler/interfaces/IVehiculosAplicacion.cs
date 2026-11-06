using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IVehiculosAplicacion
    {
        Vehiculos Insertar(Vehiculos entidad);
        List<Vehiculos> Consultar();
        List<Vehiculos> Buscar(Vehiculos entidad);

        Vehiculos Actualizar(Vehiculos entidad);
        Vehiculos Borrar(Vehiculos entidad);
    }
}
