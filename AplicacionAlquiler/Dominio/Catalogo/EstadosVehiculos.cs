using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.Dominio.Catalogo
{
    public class EstadosVehiculos
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public List<Vehiculos>? Vehiculos { get; set; }



    }
}