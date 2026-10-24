using AplicacionAlquiler.Dominio.Entidades;
using System.Text.Json.Serialization;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class EstadosUsuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        [JsonIgnore] public List<Usuarios>? Usuarios { get; set; }

    }
}
