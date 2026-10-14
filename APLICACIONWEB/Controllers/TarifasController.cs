using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TarifasController : ControllerBase
    {
        private readonly ITarifasAplicacion aplicacion;

        public TarifasController(ITarifasAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpGet]
        public List<Tarifas> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Tarifas Insertar([FromBody] Tarifas entidad) => aplicacion.Insertar(entidad);

        [HttpPut]
        public Tarifas Actualizar([FromBody] Tarifas entidad) => aplicacion.Actualizar(entidad);

        [HttpDelete]
        public Tarifas Borrar([FromBody] Tarifas entidad) => aplicacion.Borrar(entidad);
    }
}
