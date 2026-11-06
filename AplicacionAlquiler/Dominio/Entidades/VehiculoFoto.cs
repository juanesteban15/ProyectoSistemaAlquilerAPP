using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AplicacionAlquiler.Dominio.Entidades
{
        public class VehiculoFoto
    {
        public int Id { get; set; }
        public string? Ruta { get; set; } 
        public int? Vehiculo { get; set; }
        [JsonIgnore] [ForeignKey("Vehiculo")] public Vehiculos _Vehiculo { get; set; }


    }
}
