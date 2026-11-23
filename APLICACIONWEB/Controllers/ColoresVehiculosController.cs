using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ColoresVehiculosController : ControllerBase
    {
        private readonly IColoresVehiculosAplicacion aplicacion;

        public ColoresVehiculosController(IColoresVehiculosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<ColoresVehiculos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public ColoresVehiculos Insertar([FromBody] ColoresVehiculos entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public ColoresVehiculos Actualizar([FromBody] ColoresVehiculos entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public ColoresVehiculos Borrar([FromBody] ColoresVehiculos entidad) => aplicacion.Borrar(entidad);
    }
}
