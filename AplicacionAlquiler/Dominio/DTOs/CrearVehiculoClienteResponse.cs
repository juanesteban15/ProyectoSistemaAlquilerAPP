using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAlquiler.Dominio.DTOs
{
    public class CrearVehiculoClienteResponse
    {
        public int VehiculoId { get; set; }
        public string? NombreUsuario { get; set; }
        public int TarifaId { get; set; }


    }
}
