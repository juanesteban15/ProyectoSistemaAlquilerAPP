using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstadosReservasController : ControllerBase
    {
        private readonly IEstadosReservasAplicacion aplicacion;

        public EstadosReservasController(IEstadosReservasAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<EstadosReservas> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public EstadosReservas Insertar([FromBody] EstadosReservas entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public EstadosReservas Actualizar([FromBody] EstadosReservas entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public EstadosReservas Borrar([FromBody] EstadosReservas entidad) => aplicacion.Borrar(entidad);
    }
}
