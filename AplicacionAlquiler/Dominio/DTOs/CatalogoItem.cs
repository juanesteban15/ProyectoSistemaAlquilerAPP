namespace AplicacionAlquiler.Dominio.DTOs
{
    public class CatalogoItem
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int? TipoVehiculoId { get; set; }
    }
}
