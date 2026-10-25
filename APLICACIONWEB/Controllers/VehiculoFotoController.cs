using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehiculoFotoController : ControllerBase
    {
        private readonly IVehiculoFotoAplicacion aplicacion;

        public VehiculoFotoController(IVehiculoFotoAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<VehiculoFoto> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public VehiculoFoto Insertar([FromBody] VehiculoFoto entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public VehiculoFoto Actualizar([FromBody] VehiculoFoto entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public VehiculoFoto Borrar([FromBody] VehiculoFoto entidad) => aplicacion.Borrar(entidad);
    }
}
