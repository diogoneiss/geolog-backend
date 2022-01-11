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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/GeoLog/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _usuarioRepository = unitOfWork.Usuarios;
        }

        // GET: api/Usuario/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string email)
        {
            var user = await _usuarioRepository.FindFirst(u => u.Nome == email);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarSenha(UsuarioDto usuario)
        {

            //Filter specific claim    
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_bearer_token);
            var email = token.Claims.FirstOrDefault(x => x.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;

            //verificar se o email do jwt bate com o email do usuario
            var buscado = await _usuarioRepository.FindFirst(usuario => usuario.Nome == email );

            if(buscado == null)
            {
                return Unauthorized();
            }

            Usuario atualizado = new Usuario(buscado.Nome, usuario.Senha);
            //re-atualizar o id antigo
            atualizado.Id = buscado.Id;
            await _usuarioRepository.Update(atualizado);


            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDto usuarioDto)
        {

            Usuario novoUser = new Usuario(usuarioDto.Nome, usuarioDto.Senha);

            var buscado = await _usuarioRepository.FindFirst(usuario => usuario.Nome == novoUser.Nome);
            if (buscado != null)
                ModelState.AddModelError("Login", "Já existe um usuário com este login");
            
            if (ModelState.ErrorCount > 0)
                return BadRequest(ModelState);


           await _usuarioRepository.Add(novoUser);

            var token = SecurityService.GenerateToken(usuarioDto);
            return CreatedAtAction("GetUsuario", new { email = novoUser.Nome }, new { token, novoUser });
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var user = await _usuarioRepository.FindById(id);

            if (user == null)
            {
                return NotFound();
            }


            await _usuarioRepository.Remove(user);
            return NoContent();
        }

        private async Task<bool> UsuarioExists(Guid id)
        {
            var user = await _usuarioRepository.FindById(id);

            return user == null;
        }
    }
}
