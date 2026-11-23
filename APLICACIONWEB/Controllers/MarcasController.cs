using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcasAplicacion aplicacion;

        public MarcasController(IMarcasAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<Marcas> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Marcas Insertar([FromBody] Marcas entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public Marcas Actualizar([FromBody] Marcas entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public Marcas Borrar([FromBody] Marcas entidad) => aplicacion.Borrar(entidad);
    }
}
