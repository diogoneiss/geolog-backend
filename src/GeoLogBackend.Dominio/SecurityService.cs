using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Infraestrutura;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BC = BCrypt.Net.BCrypt;
namespace GeoLogBackend.GeoLogBackend.Dominio
{
    public static class SecurityService
    {
        public static string CriarHash(string senha)
        {
            string passwordHash = BC.HashPassword(senha);
            return passwordHash;
        }

       

        public static bool VerificarHash(string senha, string senhaHasheada)
        {

            return BC.Verify(senha, senhaHasheada);

        }

        public static string GenerateToken(UsuarioDto usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, usuario.Nome)

                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

