using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservasAplicacion aplicacion;

        public ReservasController(IReservasAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpGet]
        public List<Reservas> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Reservas Insertar([FromBody] Reservas entidad) => aplicacion.Insertar(entidad);

        [HttpPut]
        public Reservas Actualizar([FromBody] Reservas entidad) => aplicacion.Actualizar(entidad);

        [HttpDelete]
        public Reservas Borrar([FromBody] Reservas entidad) => aplicacion.Borrar(entidad);
    }
}
