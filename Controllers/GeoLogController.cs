using GeoLogBackend.Dominio.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoLogBackend.Controllers
{
    [ApiController]
    [Route("v1/GeoLog/Paises")]
    public class GeoLogController : ControllerBase
    {
        private IIbgeProvider _ibgeProvider;

        public GeoLogController(IIbgeProvider ibgeProvider)
        {
            _ibgeProvider = ibgeProvider;
        }

       [HttpGet("{paises}")]
       public async Task<ActionResult<string>> ObterPaisesIBGE([FromRoute]string paises)
        {
            var resultado = await _ibgeProvider.ObterPaisesIBGE(paises);

            return Ok(resultado);
        }
    }
}
