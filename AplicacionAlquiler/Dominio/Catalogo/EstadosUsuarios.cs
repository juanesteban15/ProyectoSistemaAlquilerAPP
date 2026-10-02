using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class EstadosUsuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public List<Usuarios>? Usuarios { get; set; }

    }
}
