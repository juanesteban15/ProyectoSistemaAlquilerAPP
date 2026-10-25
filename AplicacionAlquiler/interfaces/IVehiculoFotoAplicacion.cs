using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IVehiculoFotoAplicacion
    {
        VehiculoFoto Insertar(VehiculoFoto entidad);
        List<VehiculoFoto> Consultar();
        VehiculoFoto Actualizar(VehiculoFoto entidad);
        VehiculoFoto Borrar(VehiculoFoto entidad);
    }
}
