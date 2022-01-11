using GeoLogBackend.GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Controllers
{
  
    [Route("v1/GeoLog/auth")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository;

        public AutenticacaoController(IUnitOfWork unitOfWork)
        {
            _usuarioRepository = unitOfWork.Usuarios;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar(LoginRequest loginRequest)
        {

            var buscado = await _usuarioRepository.FindFirst(usuario => usuario.Nome == loginRequest.Email);

            if (buscado is null)
                return NotFound("Usuário e/ou senha inválidos");


            var user = await _usuarioRepository.FindById(buscado.Id);


            var login = new UsuarioDto(
                 loginRequest.Email,
                loginRequest.Senha

            );

            var senhaHasheada = SecurityService.CriarHash(loginRequest.Senha);
            bool senhaValida = SecurityService.VerificarHash(loginRequest.Senha, buscado.Senha);


            if (!senhaValida)
                return NotFound("Usuário e/ou senha inválidos");

            /* Como não estou modificando nada, está ok deixar comentado por enquanto
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            */
            var token = SecurityService.GenerateToken(login);



            return Ok(new
            {
                token,
                login
                
            });
        }
    }
}
