using AplicacionAlquiler.Dominio.Entidades;
using System.Text.Json.Serialization;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class MetodosPagos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore] public List<Reservas>? Reservas { get; set; }
    }
}
