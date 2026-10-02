using AplicacionAlquiler.Dominio.Catalogo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionAlquiler.Dominio.Entidades
{
    public class Vehiculos
    {
        public int Id { get; set; }
        public string? Placa { get;  set; }
        public int? Propietario { get; set; }
        public int? EstadoVehiculo { get; set; }
        public int? TipoVehiculo { get;  set; }
        public int? Marca { get; set; }
        public int? ColorVehiculo { get; set; }
        public int? SistemaTransmision { get; set; }
        public int? Categoria { get; set; }
        public int? TipoCombustible { get; set; }
        public int? Modelo { get; set; }
        public DateTime FechaRegistro { get; set; }



        //foreign key
        [ForeignKey("TipoVehiculo")] public TiposVehiculos _TipoVehiculo { get; set; }
        [ForeignKey("ColorVehiculo")] public ColoresVehiculos _ColorVehiculo { get;  set; }
        [ForeignKey("EstadoVehiculo")] public EstadosVehiculos _EstadoVehiculo { get;  set; } 
        [ForeignKey("Marca")] public Marcas _Marca { get;  set; }
        [ForeignKey("Propietario")] public Usuarios _Propietario { get;  set; }
        [ForeignKey("SistemaTransmision")] public SistemasTransmisiones _SistemaTransmision { get;  set; }
        [ForeignKey("Categoria")] public Categorias _Categoria { get;  set; }
        [ForeignKey("TipoCombustible")] public TiposCombustibles _TipoCombustible { get;  set; }


        public List<Reservas>? Reservas { get; set; }
        public List<Tarifas>? Tarifas { get; set; }

    }
}
