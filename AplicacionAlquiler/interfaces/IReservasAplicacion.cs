using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IReservasAplicacion
    {
        Reservas Insertar(Reservas entidad);
        List<Reservas> Consultar();
        Reservas Actualizar(Reservas entidad);
        Reservas Borrar(Reservas entidad);
    }
}
