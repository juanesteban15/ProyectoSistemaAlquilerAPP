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

        [HttpGet]
        public List<Personas> Consultar()
        {
            return this.IPersonasAplicacion.Consultar();
        }

        [HttpPost]
        public Personas Insertar(Personas entidad)
        {
            return this.IPersonasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Personas Actualizar(Personas entidad)
        {
            return this.IPersonasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Personas Borrar(Personas entidad)
        {
            return this.IPersonasAplicacion.Borrar(entidad);
        }
    }
}
