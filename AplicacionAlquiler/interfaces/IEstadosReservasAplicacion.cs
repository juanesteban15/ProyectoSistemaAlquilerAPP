using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface IEstadosReservasAplicacion
    {
        EstadosReservas Insertar(EstadosReservas entidad);
        List<EstadosReservas> Consultar();
        EstadosReservas Actualizar(EstadosReservas entidad);
        EstadosReservas Borrar(EstadosReservas entidad);
    }
}
