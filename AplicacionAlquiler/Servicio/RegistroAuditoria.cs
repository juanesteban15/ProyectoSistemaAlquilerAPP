namespace AplicacionAlquiler.Servicio
{
    
        public class RegistroAuditoria
        {
            public int Id { get; set; }
            public DateTime FechaUtc { get; set; }
            public string Movimiento { get; set; }
            public string Detalle { get; set; } 

    }

}