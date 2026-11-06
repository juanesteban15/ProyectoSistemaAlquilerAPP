using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAlquiler.Dominio.DTOs
{
    public class LoginRequest
    {
        public string Identificador { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
