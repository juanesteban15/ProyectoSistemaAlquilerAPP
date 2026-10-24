using AplicacionAlquiler.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class Marcas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoVehiculoId { get; set; }

        [ForeignKey("TipoVehiculoId")] public TiposVehiculos TipoVehiculo { get; set; }
        [JsonIgnore] public List<Vehiculos>? Vehiculos { get; set; }


    }
}
