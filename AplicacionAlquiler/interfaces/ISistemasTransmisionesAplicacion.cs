using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.interfaces
{
    public interface ISistemasTransmisionesAplicacion
    {
        SistemasTransmisiones Insertar(SistemasTransmisiones entidad);
        List<SistemasTransmisiones> Consultar();
        SistemasTransmisiones Actualizar(SistemasTransmisiones entidad);
        SistemasTransmisiones Borrar(SistemasTransmisiones entidad);
    }
}
