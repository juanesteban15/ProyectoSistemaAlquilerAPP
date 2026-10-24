using AplicacionAlquiler.Dominio.Catalogo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace AplicacionAlquiler.Dominio.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int? Persona { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Rol { get; set; }
        public int? Estadousuario { get; set; }
        public DateTime? FechaEliminacion { get; set; } 


        public DateTime? FechaCreacion { get; set; }




        //-------FOREIGN KEY---------//
        [ForeignKey("Persona")] public Personas _Persona { get; set; }

        [ForeignKey("Estadousuario")] public EstadosUsuarios? _EstadoUsuario { get; set; }

        [JsonIgnore] public List<Vehiculos>? Vehiculos { get; set; }
        [JsonIgnore]  public List<Reservas>? Reservas { get; set; }






    }
}
