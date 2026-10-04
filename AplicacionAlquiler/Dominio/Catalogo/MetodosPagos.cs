using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class MetodosPagos
    {
        public int Id { get;  set; }
        public string Nombre { get;  set; }
        public List<Reservas>? Reservas { get; set; }
    }
}
