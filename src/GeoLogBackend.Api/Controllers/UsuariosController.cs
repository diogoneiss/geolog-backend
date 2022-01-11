using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Controllers
{
    [ApiController]
    [Route("v1/GeoLog/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _usuarioRepository = unitOfWork.Usuarios;
        }

        [HttpPost]
        public async Task<ActionResult> CriarUsuario([FromBody] Usuario Usuario)
        {
            await _usuarioRepository.Add(Usuario);

            return Ok();
        }

        [HttpGet("/validarLogin")]
        public async Task<ActionResult<bool>> ValidarLogIn([FromBody] Usuario Usuario)
        {
            bool loginValiido = await _usuarioRepository.validarLogin(Usuario);

            return Ok(loginValiido);
        }

        [HttpPatch("/alterarSenha/{usuario}")]
        public async Task<ActionResult> AtualizarSenha([FromBody] string usuario)
        {
            await _usuarioRepository.alterarSenha(usuario);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> ApagarUsuario([FromBody] Usuario Usuario)
        {
            //Validar se é ideal receber usuário completo
            await _usuarioRepository.Remove(Usuario);

            return Ok();
        }
    }
}
