using System.ComponentModel.DataAnnotations.Schema;
using AplicacionAlquiler.Dominio.Catalogo;

namespace AplicacionAlquiler.Dominio.Entidades
{
    public class Reservas
    {
        public int Id { get;  set; }

        public int Vehiculo { get;  set; }
        public int Cliente { get;  set; }
        public int EstadoReserva { get;  set; }
        public DateTime FechaInicio { get;  set; }
        public DateTime FechaFin { get;  set; }
        public int MetodoPago { get;  set; }

        public int Tarifa { get; set; }

        public decimal PrecioTotal { get;  set; }
        public string? Observaciones { get;  set; }
        public DateTime FechaCreacion { get;  set; }


        //----Foreign Keys----//

      [ForeignKey("Vehiculo")]  public Vehiculos _Vehiculo { get;  set; }
      [ForeignKey("Cliente")] public Usuarios _Cliente { get;  set; }
      [ForeignKey("EstadoReserva")] public EstadosReservas _EstadoReserva { get;  set; }
      [ForeignKey("MetodoPago")] public MetodosPagos _MetodoPago { get; set; }
      [ForeignKey("Tarifa")] public Tarifas _Tarifa { get; set; }





    }
}
