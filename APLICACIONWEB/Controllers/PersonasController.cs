using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonasController : ControllerBase
    {
        private IPersonasAplicacion IPersonasAplicacion;

        public PersonasController(IPersonasAplicacion IPersonasAplicacion)
        {
            this.IPersonasAplicacion = IPersonasAplicacion;
        }

        [HttpPost]
        public List<Personas> Consultar()
        {
            return this.IPersonasAplicacion.Consultar();
        }

        [HttpPost]
        public Personas Insertar([FromBody] Personas entidad)
        {
            return this.IPersonasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Personas Actualizar([FromBody] Personas entidad)
        {
            return this.IPersonasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Personas Borrar([FromBody] Personas entidad)
        {
            return this.IPersonasAplicacion.Borrar(entidad);
        }
    }
}
