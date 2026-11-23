using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstadosVehiculosController : ControllerBase
    {
        private readonly IEstadosVehiculosAplicacion aplicacion;

        public EstadosVehiculosController(IEstadosVehiculosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<EstadosVehiculos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public EstadosVehiculos Insertar([FromBody] EstadosVehiculos entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public EstadosVehiculos Actualizar([FromBody] EstadosVehiculos entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public EstadosVehiculos Borrar([FromBody] EstadosVehiculos entidad) => aplicacion.Borrar(entidad);
    }
}
