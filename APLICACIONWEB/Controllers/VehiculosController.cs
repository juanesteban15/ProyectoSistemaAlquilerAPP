using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculosAplicacion aplicacion;

        public VehiculosController(IVehiculosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpGet]
        public List<Vehiculos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Vehiculos Insertar([FromBody] Vehiculos entidad) => aplicacion.Insertar(entidad);

        [HttpPut]
        public Vehiculos Actualizar([FromBody] Vehiculos entidad) => aplicacion.Actualizar(entidad);

        [HttpDelete]
        public Vehiculos Borrar([FromBody] Vehiculos entidad) => aplicacion.Borrar(entidad);
    }
}
