using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposVehiculosController : ControllerBase
    {
        private readonly ITiposVehiculosAplicacion aplicacion;

        public TiposVehiculosController(ITiposVehiculosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<TiposVehiculos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public TiposVehiculos Insertar([FromBody] TiposVehiculos entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public TiposVehiculos Actualizar([FromBody] TiposVehiculos entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public TiposVehiculos Borrar([FromBody] TiposVehiculos entidad) => aplicacion.Borrar(entidad);
    }
}
