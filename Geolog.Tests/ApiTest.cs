using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GeoLogBackend.Api.Tests
{
    [Collection(nameof(IntegrationApiFixtureCollection))]
    public class ApiTest : IClassFixture<WebApplicationFactory<StartupTest>>
    {

        private readonly IntegrationTestsFixture<StartupTest> _testsFixture;

        public ApiTest(IntegrationTestsFixture<StartupTest> testsFixture)
        {
            _testsFixture = testsFixture;

        }


        [Fact(DisplayName = "Solicitação de rota inexistente")]
        [Trait("IntegrationTests", "PostRequest")]

        public async void Enviar_Requisicao_E_Servidor_Valida()
        {


          var response = await _testsFixture.Client.GetAsync("/essa-rota-nao-existe");


            //eu quero que dê 404, já que a rota não existe
            Assert.Equal(((int)response.StatusCode), StatusCodes.Status404NotFound);
        }


        [Fact(DisplayName = "Teste executado roda sem erros")]
        [Trait("TestSetup", "EmptyAssert")]
        public void Teste_QuandoExecutado_RodaSemErros()
        {
            Assert.True(true);
        }
    }
}
