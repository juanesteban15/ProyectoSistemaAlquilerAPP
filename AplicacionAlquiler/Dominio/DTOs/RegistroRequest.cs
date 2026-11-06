using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AplicacionAlquiler.Dominio.DTOs
{
    public class RegistroRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? PaisNacimiento { get; set; }
        [Required(ErrorMessage = "Selecciona tu fecha de nacimiento.")]
        public DateTime? FechaNacimiento { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Genero { get; set; }
        public string? ComplementoPais { get; set; }
        public string? Telefono { get; set; }

        // Datos de Usuarios
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }

    }
}
