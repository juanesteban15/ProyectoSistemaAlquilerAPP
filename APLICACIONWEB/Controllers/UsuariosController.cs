using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosAplicacion aplicacion;

        public UsuariosController(IUsuariosAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<Usuarios> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Usuarios Insertar([FromBody] Usuarios entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public Usuarios Actualizar([FromBody] Usuarios entidad) => aplicacion.Actualizar(entidad);

        [HttpDelete]
        public Usuarios Borrar([FromBody] Usuarios entidad) => aplicacion.Borrar(entidad);
    }
}
