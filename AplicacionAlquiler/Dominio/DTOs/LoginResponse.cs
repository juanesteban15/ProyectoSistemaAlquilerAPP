using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAlquiler.Dominio.DTOs
{
    public class LoginResponse
    {
        public bool Autenticado { get; set; }
        public int? Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Rol { get; set; }
    }
}
