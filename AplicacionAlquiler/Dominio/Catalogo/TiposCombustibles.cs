using AplicacionAlquiler.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class TiposCombustibles
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int TipoVehiculo { get; private set; }
        public List<Vehiculos>? Vehiculos { get; set; }

    }
}