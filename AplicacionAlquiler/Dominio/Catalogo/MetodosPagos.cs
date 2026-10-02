using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class MetodosPagos
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public List<Reservas>? Reservas { get; set; }
    }
}
