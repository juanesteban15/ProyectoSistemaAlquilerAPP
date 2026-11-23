using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SistemasTransmisionesController : ControllerBase
    {
        private readonly ISistemasTransmisionesAplicacion aplicacion;

        public SistemasTransmisionesController(ISistemasTransmisionesAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<SistemasTransmisiones> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public SistemasTransmisiones Insertar([FromBody] SistemasTransmisiones entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public SistemasTransmisiones Actualizar([FromBody] SistemasTransmisiones entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public SistemasTransmisiones Borrar([FromBody] SistemasTransmisiones entidad) => aplicacion.Borrar(entidad);
    }
}
