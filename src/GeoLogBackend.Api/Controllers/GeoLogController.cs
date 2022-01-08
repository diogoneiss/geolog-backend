using GeoLogBackend.Dominio;
using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GeoLogBackend.Api.Controllers
{

    public class Demo : Entidade, IAggregateRoot
    {
         
    }

    [ApiController]
    [Route("v1/GeoLog/Paises")]
    public class GeoLogController : ControllerBase
    {
        private IIbgeProvider _ibgeProvider;


        private readonly IPaisRepository _paisRepository;
        

       

        public GeoLogController(IIbgeProvider ibgeProvider, IUnitOfWork uow)
        {
            _ibgeProvider = ibgeProvider;
            _paisRepository = uow.Paises;
        }

       [HttpGet("{paises}")]
       public async Task<ActionResult<string>> ObterPaisesIBGE([FromRoute]string paises)
        {
            var resultado = await _ibgeProvider.ObterPaisesIBGE(paises);

            Pais primeiro = resultado[new Random().Next(resultado.Count)];

            await _paisRepository.Add(primeiro);


            return Ok(primeiro);
        }
    }
}
