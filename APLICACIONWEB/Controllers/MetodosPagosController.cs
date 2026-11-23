using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MetodosPagosController : ControllerBase
    {
        private readonly IMetodosPagosAplicacion aplicacion;

        public MetodosPagosController(IMetodosPagosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<MetodosPagos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public MetodosPagos Insertar([FromBody] MetodosPagos entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public MetodosPagos Actualizar([FromBody] MetodosPagos entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public MetodosPagos Borrar([FromBody] MetodosPagos entidad) => aplicacion.Borrar(entidad);
    }
}
