using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface ITarifasPresentacion
    {
        Tarifas Insertar(Tarifas entidad);
        Tarifas Actualizar(Tarifas entidad);
        List<Tarifas> Consultar();
        Tarifas Borrar(Tarifas entidad);
    }
}
