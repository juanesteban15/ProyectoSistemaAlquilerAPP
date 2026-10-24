using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AplicacionAlquiler.Dominio.Entidades
{
    public class Tarifas
    {
        public int Id { get; set; }
        public int Vehiculo { get; set; }
        public decimal PrecioPorDia { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Activa { get; set; }


        [ForeignKey("Vehiculo")] public Vehiculos _Vehiculo { get; set; }

        [JsonIgnore] public List<Reservas>? Reservas { get; set; }




    }
}
