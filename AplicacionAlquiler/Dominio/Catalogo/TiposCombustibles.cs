using AplicacionAlquiler.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class TiposCombustibles
    {
        public int Id { get;  set; }
        public string Nombre { get;  set; }
        public int TipoVehiculo { get;  set; }
        public List<Vehiculos>? Vehiculos { get; set; }

    }
}