using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer")]
    [Route("v1/GeoLog/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _usuarioRepository = unitOfWork.Usuarios;
        }

        /// <summary>
        /// [Autenticado] Recupera o usuário que corresponde ao email enviado. Requer autenticação.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Usuário encontrado</returns>
        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Usuario>> GetUsuario(string email)
        {
            var user = await _usuarioRepository.FindFirst(u => u.Nome == email);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// [Autenticado] Atualiza a senha do usuário. Requer autenticação e que o JWT corresponda ao usuário que se quer alterar
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>NoContent caso tudo dê certo.</returns>
        [HttpPatch("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]


        public async Task<IActionResult> AtualizarSenha(UsuarioDto usuario)
        {

            //Filter specific claim    
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_bearer_token);
            var email = token.Claims.FirstOrDefault(x => x.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;

            //verificar se o usuário existe, nao da pra atualizar um user que nao existe ainda
            var buscado = await _usuarioRepository.FindFirst(u => u.Nome == usuario.Nome);


            if (buscado is null)
            {
                return NotFound("Usuário com o email informado não existe");
            }


            //verificar se o email do jwt bate com o email do usuario que recuperamos
            if (buscado.Nome != email )
            {
                return Unauthorized("Usuário autenticado com JWT não corresponde ao usuário a ser atualizado");
            }

            try
            {
                await _usuarioRepository.alterarSenha(buscado, usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Atualiza a senha do usuário. Não requer autenticação. Desejável receber código de validação
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>NoContent caso tudo dê certo.</returns>
        [HttpPatch("esqueciSenha/{codigo}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [AllowAnonymous]

        public async Task<IActionResult> EsqueciSenha([FromBody] UsuarioDto usuario, [FromRoute] int codigo )
        {


            //aqui entra a lógica de validação do código.
            //nao existe pq nao enviamos emails


            //verificar se o usuário existe, nao da pra atualizar um user que nao existe ainda
            var buscado = await _usuarioRepository.FindFirst(u => u.Nome == usuario.Nome);


            if (buscado is null)
            {
                return NotFound("Usuário com o email informado não existe");
            }



            Usuario atualizado = new Usuario(usuario.Nome, usuario.Senha);

            //re-atualizar o id antigo e data de criação, são modificados no construtor
            atualizado.Id = buscado.Id;
            atualizado.CreatedAt = buscado.CreatedAt;

            await _usuarioRepository.Update(atualizado);


            return NoContent();
        }


        /// <summary>
        /// Cria um usuário e retorna seu token jwt e dados criados
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDto usuarioDto)
        {

            Usuario novoUser = new Usuario(usuarioDto.Nome, usuarioDto.Senha);

            var buscado = await UsuarioExists(usuarioDto.Nome);
            if (buscado == true)
                ModelState.AddModelError("Login", "Já existe um usuário com este login");
            
            if (ModelState.ErrorCount > 0)
                return BadRequest(ModelState);


           await _usuarioRepository.Add(novoUser);

            var token = SecurityService.GenerateToken(usuarioDto);
            return CreatedAtAction("GetUsuario", new { email = novoUser.Nome }, new { token, novoUser });
        }



        /// <summary>
        /// [Autenticado] Deleta o usuario que corresponde ao seguinte GUID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {

            //Filter specific claim    
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_bearer_token);
            var email = token.Claims.FirstOrDefault(x => x.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;

            //verificar se o usuário existe, nao da pra atualizar um user que nao existe ainda
            var buscado = await _usuarioRepository.FindById(id);


            if (buscado is null)
            {
                return NotFound("Usuário com o email informado não existe");
            }


            //verificar se o email do jwt bate com o email do usuario que recuperamos
            if (buscado.Nome != email)
            {
                return Unauthorized("Usuário autenticado com JWT não corresponde ao usuário a ser deletado");
            }



            await _usuarioRepository.Remove(buscado);
            return NoContent();
        }

        private async Task<bool> UsuarioExists(Guid id)
        {
            var user = await _usuarioRepository.FindById(id);

            return user != null;
        }

        private async Task<bool> UsuarioExists(string email)
        {
            var user = await _usuarioRepository.FindFirst(u => u.Nome == email);

            return user != null;
        }
    }
}
