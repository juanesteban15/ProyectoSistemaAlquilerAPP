using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstadosUsuariosController : ControllerBase
    {
        private readonly IEstadosUsuariosAplicacion aplicacion;

        public EstadosUsuariosController(IEstadosUsuariosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<EstadosUsuarios> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public EstadosUsuarios Insertar([FromBody] EstadosUsuarios entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public EstadosUsuarios Actualizar([FromBody] EstadosUsuarios entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public EstadosUsuarios Borrar([FromBody] EstadosUsuarios entidad) => aplicacion.Borrar(entidad);
    }
}
