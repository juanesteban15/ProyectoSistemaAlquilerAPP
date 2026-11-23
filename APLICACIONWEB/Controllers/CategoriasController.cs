using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasAplicacion aplicacion;

        public CategoriasController(ICategoriasAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpPost]
        public List<Categorias> Consultar() => aplicacion.Consultar();

        [HttpPost]
        public Categorias Insertar([FromBody] Categorias entidad) => aplicacion.Insertar(entidad);

        [HttpPost]
        public Categorias Actualizar([FromBody] Categorias entidad) => aplicacion.Actualizar(entidad);

        [HttpPost]
        public Categorias Borrar([FromBody] Categorias entidad) => aplicacion.Borrar(entidad);
    }
}
