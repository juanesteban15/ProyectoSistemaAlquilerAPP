using AplicacionAlquiler.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;


namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class Marcas
    {
        public int Id { get;  set; }
        public string Nombre { get;  set; }
        public int TipoVehiculoId { get;  set; }

        [ForeignKey("TipoVehiculoId")] public TiposVehiculos TipoVehiculo { get;  set; }
        public List<Vehiculos>? Vehiculos { get; set; }


    }
}
