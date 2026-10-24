
using AplicacionAlquiler.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class Categorias
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public int TipoVehiculo { get; set; }


        [ForeignKey("TipoVehiculo")] public TiposVehiculos _TipoVehiculo { get; set; }

        [JsonIgnore] public List<Vehiculos>? Vehiculos { get; set; }


    }
}
