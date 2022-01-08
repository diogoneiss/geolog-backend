using GeoLogBackend.GeoLogBackend.Api.Validations;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace GeoLogBackend.Api.Tests
{
   public class ValidationTests
    {
        private readonly GetPaisDtoValidation validation;

        public ValidationTests()
        {
            validation = new GetPaisDtoValidation();
        }
        [Theory]
        [Trait("Pais", "PaisGetRequest")]
        [InlineData("", false)]
        [InlineData("aa", false)]
        [InlineData("andorra", true)]
        [InlineData("andorra1", false)]
        [InlineData("11121", false)]
        [InlineData("21", false)]



        public void DeveValidarNome(string nome, bool aprovar)

        {
            var request = new PaisGetDto(nome);

            bool ehInstaciaValida = validation.Validate(request).IsValid;


            Assert.Equal(aprovar, ehInstaciaValida);

        }
    }
}
