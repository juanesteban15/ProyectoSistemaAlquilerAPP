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

        [HttpPost]
        public List<Vehiculos> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Vehiculos Insertar([FromBody] Vehiculos entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public Vehiculos Actualizar([FromBody] Vehiculos entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public Vehiculos Borrar([FromBody] Vehiculos entidad) => aplicacion.Borrar(entidad);
    }
}
