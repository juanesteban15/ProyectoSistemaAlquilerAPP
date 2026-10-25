using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IVehiculoFotoPresentacion
    {
        VehiculoFoto Insertar(VehiculoFoto entidad);
        VehiculoFoto Actualizar(VehiculoFoto entidad);
        List<VehiculoFoto> Consultar();
        VehiculoFoto Borrar(VehiculoFoto entidad);
    }
}
