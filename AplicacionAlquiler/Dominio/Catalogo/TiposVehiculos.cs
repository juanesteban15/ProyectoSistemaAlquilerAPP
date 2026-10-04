using AplicacionAlquiler.Dominio.Entidades;
namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class TiposVehiculos
    {
        public int Id { get;  set; }
        public string Nombre { get;  set; }
        public List<Vehiculos>? Vehiculos { get; set; }
        public List<Marcas>? marcas { get; set; }



    }
}