using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAlquiler.Dominio.DTOs
{
    public class CrearVehiculoClienteRequest
    {
        public string Placa { get; set; } 

        public int TipoVehiculoId { get; set; }
        public int MarcaId { get; set; }
        public int TipoCombustibleId { get; set; }
        public int ColorVehiculoId { get; set; }
        public int SistemaTransmisionId { get; set; }
        public int CategoriaId { get; set; }
        public int Modelo { get; set; }

        public decimal PrecioPorDia { get; set; }
        public DateTime FechaInicioTarifa { get; set; }

        // Se asigna desde la sesión en el servidor, no desde un campo editable.
        public int PropietarioId { get; set; }

        public string? FotoBase64 { get; set; }
    }
}
