using AplicacionAlquiler.Dominio.Entidades;
namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class TiposVehiculos
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public List<Vehiculos>? Vehiculos { get; set; }
        public List<Marcas>? marcas { get; set; }



    }
}