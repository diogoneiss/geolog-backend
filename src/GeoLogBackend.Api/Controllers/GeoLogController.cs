using GeoLogBackend.Dominio;
using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        /// <summary>
        /// Recupera um pais dentro da lista do IBGE
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Pais recuperado</returns>
        [HttpGet("{pais}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]


        public async Task<ActionResult<PaisResponseDto>> ObterPaisesIBGE([FromRoute] string pais)
        {


            var paisValidado = new PaisGetDto(pais, ModelState);

            var resultado = await _ibgeProvider.ObterPaisesIBGE(paisValidado.Nome);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Se não encontrar dados para o país, basta retornar o array vazio
            if (!resultado.Any())
            {
                return Ok(resultado);
            }


            Pais primeiro = resultado[new Random().Next(resultado.Count)];

            await _paisRepository.Add(primeiro);

            PaisResponseDto retorno = new PaisResponseDto(primeiro);

            return Ok(retorno);
        }

        /* TODO: Fazer retornar todos
         * revalidar todos no banco mongo (puxar todos da api e, se certo, limpar os do banco e adicionar dnv)
         * Adicinar dados custom novos de pais
         * Remover dados custom novos de pais
         * Atualizar dados custom de pais
         * Retornr dados custom de pais
         * Cadastro de novo usario
         */
    }
}
