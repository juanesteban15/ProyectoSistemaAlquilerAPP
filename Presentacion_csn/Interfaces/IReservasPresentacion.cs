using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IReservasPresentacion
    {
        List<Reservas> Consultar();
        List<Reservas> Buscar(Reservas entidad);
    }
}
