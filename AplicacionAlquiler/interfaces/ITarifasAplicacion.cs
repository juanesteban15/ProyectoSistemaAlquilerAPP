using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface ITarifasAplicacion
    {
        Tarifas Insertar(Tarifas entidad);
        List<Tarifas> Consultar();
        Tarifas Actualizar(Tarifas entidad);
        Tarifas Borrar(Tarifas entidad);
    }
}
