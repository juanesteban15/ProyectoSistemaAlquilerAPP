
namespace AplicacionAlquiler.Dominio.Entidades
{
    public class Personas
    {
        public int Id { get;  set; }
        public string? Nombre { get;  set; }
        public string? Apellido { get;  set; } 
        public string? Email { get;  set; }
        public string? PaisNacimiento { get;  set; } 
        public DateTime FechaNacimiento { get;  set; }

        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Genero { get;  set; }
        public string? ComplementoPais { get;  set; }
        public string? Telefono { get; set; }


        public List<Usuarios>? Usuarios { get; set; }

    }
}
