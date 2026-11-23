using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposCombustiblesController : ControllerBase
    {
        private readonly ITiposCombustiblesAplicacion aplicacion;

        public TiposCombustiblesController(ITiposCombustiblesAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<TiposCombustibles> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public TiposCombustibles Insertar([FromBody] TiposCombustibles entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public TiposCombustibles Actualizar([FromBody] TiposCombustibles entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public TiposCombustibles Borrar([FromBody] TiposCombustibles entidad) => aplicacion.Borrar(entidad);
    }
}
