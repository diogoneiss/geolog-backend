using System;
using System.Linq;
using System.Threading.Tasks;
using GeoLogBackend.Dominio;
using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;




    public class Demo : Entidade, IAggregateRoot
    {

    }

    [ApiController]
    [Route("v1/GeoLog/Alteracoes")]
    public class AlteracoesController : ControllerBase
    {

        private readonly ILogRepository _alteracoesRepository;





        public AlteracoesController(IIbgeProvider ibgeProvider, IUnitOfWork uow)
        {

            _alteracoesRepository = uow.Alteracoes;
          
        }

        /// <summary>
        /// Recupera todas as alteracoes de um pais
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Alteracoes recuperadas</returns>
        [HttpGet("/pais/{pais}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]


        public async Task<ActionResult<AlteracaoDto[]>> AlteracoesPorPais([FromRoute] string pais)
        {
            var paisValidado = new PaisGetDto(pais, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ver se existe um pais modificado com essa sigla
            var paisesAlterados = await _alteracoesRepository.AlteracoesPais(paisValidado.Nome);

            //Se não encontrar o país, basta retornar o array vazio
            if (!paisesAlterados.Any())
            {
                return Ok("Nao foram encontradas alteracoes para o pais escolhido");
            }

        
            var alteracaoDto = paisesAlterados.Select(x => new AlteracaoDto(x.UsuarioQueModificou, x.PaisModificado, x.ModificacaoFeita));
        
       
            return Ok(alteracaoDto);
        }

        /// <summary>
        /// Recupera as alteracoes que um usuario fez
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Pais recuperado</returns>
        [HttpPatch("/usuario/{nome}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]


        public async Task<ActionResult<AlteracaoDto[]>> AlteracoesPorUsuario(string nome)
        {


        

      

        //ver se existe um pais modificado com essa sigla
        var alteracoesUsuario = await _alteracoesRepository.AlteracoesUsuario(nome);

        //Se não encontrar o país, basta retornar o array vazio
        if (!alteracoesUsuario.Any())
        {
            return Ok("Nao foram encontradas alteracoes para o usuario escolhido");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var alteracaoDto = alteracoesUsuario.Select(x => new AlteracaoDto(x.UsuarioQueModificou, x.PaisModificado, x.ModificacaoFeita));


        return Ok(alteracaoDto);
    }

      
}

