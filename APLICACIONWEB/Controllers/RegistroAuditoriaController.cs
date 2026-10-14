using AplicacionAlquiler.Servicio;
using AplicacionAlquiler.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APLICACIONWEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RegistroAuditoriaController : ControllerBase
    {
        private readonly IRegistroAuditoriaAplicacion aplicacion;

        public RegistroAuditoriaController(IRegistroAuditoriaAplicacion aplicacion)
        {
            this.aplicacion = aplicacion;
        }

        [HttpGet]
        public List<RegistroAuditoria> Consultar() => aplicacion.Consultar();
    }
}
