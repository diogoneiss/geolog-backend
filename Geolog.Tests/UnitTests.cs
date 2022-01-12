using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Api.Validations;
using GeoLogBackend.GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace GeoLogBackend.Api.Tests
{
   public class UnitTests
    {
        private readonly GetPaisDtoValidation validation;

        public UnitTests()
        {
            validation = new GetPaisDtoValidation();
        }
        [Theory]
        [Trait("Pais", "PaisGetRequest")]
        [InlineData("", false)]
        [InlineData("aa", true)]
        [InlineData("andorra", false)]
        [InlineData("andorra1", false)]
        [InlineData("11121", false)]
        [InlineData("21", false)]



        public void DeveValidarNome(string nome, bool aprovar)

        {
            var request = new PaisGetDto(nome);

            bool ehInstaciaValida = validation.Validate(request).IsValid;


            Assert.Equal(aprovar, ehInstaciaValida);

        }

        [Fact(DisplayName = "Usuario deve ter senha hasheada")]
        [Trait("Usuario", "Hash senha")]
        public void DeveHashearSenha()
        {
            var senha = "teste123456#!";
            var nome = "teste";

            var novoUser = new Usuario(nome, senha);

            Assert.NotEqual(senha, novoUser.Senha);
        }
        [Fact(DisplayName = "Usuario deve ter construtor sem hash")]
        [Trait("Usuario", "Construir sem hash")]
        public void NaoDeveHashearSenha()
        {
            var senha = "teste123456#!";
            var nome = "teste";

            var senhaHasheada = SecurityService.CriarHash(senha);


            var novoUser = new Usuario(Guid.NewGuid(), DateTime.Now, nome, senhaHasheada);

            Assert.Equal(senhaHasheada, novoUser.Senha);

        }
        [Fact(DisplayName = "Hash de senhas funciona")]
        [Trait("Seguranca", "Hash senha")]
        public void HashEValidacaoDeSenhas()
        {
            var senha = "teste123456#!";
            var nome = "teste";

            var novoUser = new Usuario(nome, senha);

            var igual = SecurityService.VerificarHash(senha, novoUser.Senha);


            Assert.True(igual);
        }

    }
}
