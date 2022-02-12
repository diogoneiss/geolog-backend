using GeoLogBackend.Dominio;
using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly ILogRepository _alteracoesRepository;
        private readonly IUsuarioRepository _usuarioRepository;





        public GeoLogController(IIbgeProvider ibgeProvider, IUnitOfWork uow)
        {
            _ibgeProvider = ibgeProvider;
            _paisRepository = uow.Paises;
            _alteracoesRepository = uow.Alteracoes;
            _usuarioRepository = uow.Usuarios;
        }



        /// <summary>
        /// Recupera todos os paises dentro da lista do IBGE
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Pais recuperado</returns>
        [HttpGet("/all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Object[]>> ObterTodosPaisesIBGE()
        {
            
            var tmp = await _ibgeProvider.ObterPaisesIBGE("");
            var resultado = tmp.Select(x => new { x.Nome.Abreviado, x.IdSequencial });
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Se não encontrar dados para o país, basta retornar o array vazio
            if (resultado is null)
            {
                return Ok(resultado);
            }



            return Ok(resultado);
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

            //ver se existe um pais modificado com essa sigla
            var paisCadastrado = await _paisRepository.FindPaisesBySigla(paisValidado.Nome);


            
            //se nao existe o pais, adicionar no banco e retornar
            //TODO: Extrair essa funcionalidade pro repositorio de pais, assim sempre são retornados todos os paises
            Pais resultado;
            if (paisCadastrado.Any())
            {
                resultado = paisCadastrado.First();
            }
            else
            {
                var tmp = await _ibgeProvider.ObterPaisesIBGE(paisValidado.Nome);
                resultado = tmp.Find(x => x.IdSequencial.ISO31661_ALPHA2 == paisValidado.Nome);
                //nao achei no alpha2? vai no 3
                if (resultado is null)
                {

                    resultado = tmp.Find(x => x.IdSequencial.ISO31661_ALPHA3 == paisValidado.Nome);

                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Se não encontrar dados para o país, basta retornar o array vazio
            if (resultado is null)
            {
                return Ok(resultado);
            }


           

            //memorizando pais
            if(!paisCadastrado.Any())
            {
                await _paisRepository.Add(resultado);
            }

            PaisResponseDto retorno = new PaisResponseDto(resultado);

            return Ok(retorno);
        }

        /// <summary>
        /// [Autenticado] Modifica um pais dentro da lista do IBGE
        /// </summary>
        /// <param name="pais"></param>
        /// <param name="informacao"></param>
        /// <returns>Pais recuperado</returns>
        [HttpPatch("{pais}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult<PaisResponseDto>> CadastrarAtualizacao([FromRoute] string pais, [FromBody] InformacaoPaisDto informacao)
        {

            //Talvez extrair para um util?  
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_bearer_token);
            var email = token.Claims.FirstOrDefault(x => x.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;




            var paisValidado = new PaisGetDto(pais, ModelState);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //buscar por sigla
            var paisCadastrado = await _paisRepository.FindPaisesBySigla(paisValidado.Nome);

            //se nao existe o pais, adicionar no banco e retornar
            //TODO: Extrair essa funcionalidade pro repositorio de pais, assim sempre são retornados todos os paises
            Pais resultado;
            if (paisCadastrado.Any())
            {
                resultado = paisCadastrado.First();
            }
            else
            {
                var tmp = await _ibgeProvider.ObterPaisesIBGE(paisValidado.Nome);
                resultado = tmp.Find(x => x.IdSequencial.ISO31661_ALPHA2 == paisValidado.Nome);
                //se nao achei com 2 chars
                if(resultado is null)
                {
                 resultado = tmp.Find(x => x.IdSequencial.ISO31661_ALPHA3 == paisValidado.Nome);
                    if(resultado is null)
                    {
                        return NotFound(pais);
                    }

                }
                await _paisRepository.Add(resultado);
            }
             

            try
            {
                resultado.ValidaAtualizacao(informacao);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

           

            var Usuario = await _usuarioRepository.RecuperarUsuario(email);

            if(Usuario is null)
            {
                return BadRequest($"Token para usuario {email} invalido, voce nao existe no banco de usuarios mais.");
            }

            var alteracao = new LogAlteracao(Usuario, resultado, informacao);

            await _alteracoesRepository.Add(alteracao);
            await _paisRepository.Update(resultado);
            
            PaisResponseDto retorno = new PaisResponseDto(resultado);

            return Ok(retorno);
        }

        /* TODO: Fazer retornar todos
         * Adicinar dados custom novos de pais
         * Remover dados custom novos de pais
         * Atualizar dados custom de pais
         * Retornr dados custom de pais
         * Cadastro de novo usario
         */
    }
}
