using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        [Fact(DisplayName = "Solicitação sem autenticacao")]
        [Trait("IntegrationTests", "Usuarios")]

        public async void Tentativa_De_Recuperar_Dados_Sem_Auth()
        {
            string endpoint = "/v1/GeoLog/Usuarios/teste";



            var response = await _testsFixture.Client.GetAsync(endpoint);


            //eu quero que dê 401, já que nao providenciei um token
            Assert.Equal(StatusCodes.Status401Unauthorized, (int)response.StatusCode);
        }

        [Fact(DisplayName = "Solicitação com autenticacao")]
        [Trait("IntegrationTests", "Usuarios")]

        public async void Tentativa_De_Recuperar_Dados_Com_Auth()
        {
            string endpoint = "/v1/GeoLog/Usuarios/";


            LoginRequest usuariocorreto = new LoginRequest("teste", "teste");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(usuariocorreto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _testsFixture.Client.PostAsync("/v1/GeoLog/auth", httpContent);

            var token = response.Content.ToString();
            _testsFixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", token);

             response = await _testsFixture.Client.GetAsync($"endpoint/{usuariocorreto.Email}");


            //eu quero que dê 200, já que providenciei um token
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        [Fact(DisplayName = "Solicitação de rota inexistente")]
        [Trait("IntegrationTests", "PostRequest")]

        public async void Enviar_Requisicao_E_Servidor_Valida()
        {


          var response = await _testsFixture.Client.GetAsync("/essa-rota-nao-existe");


            //eu quero que dê 404, já que a rota não existe
            Assert.Equal(((int)response.StatusCode), StatusCodes.Status404NotFound);
        }

        
        [Fact(DisplayName = "Solicitação de autenticacao valida")]
        [Trait("IntegrationTests", "AuthRequest")]

        public async void Tentativa_De_Auth_Com_User_Valido()
        {
           
            string endpoint = "/v1/GeoLog/auth";
            LoginRequest usuariocorreto = new LoginRequest("teste", "teste");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(usuariocorreto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _testsFixture.Client.PostAsync(endpoint, httpContent); 

            //nao encontrou? Sem problema, crio na mao
            if((int) response.StatusCode == StatusCodes.Status404NotFound)
            {
                //criar usuario com os valores
                await _testsFixture.Client.PostAsync("/v1/GeoLog/Usuarios", httpContent);
                response = await _testsFixture.Client.PostAsync(endpoint, httpContent);
            }

            //eu quero que dê 200, já que o usuario DE FATO existe
            Assert.Equal(((int)response.StatusCode), StatusCodes.Status200OK);
        }

        [Fact(DisplayName = "Solicitação de autenticacao invalida")]
        [Trait("IntegrationTests", "AuthRequest")]

        public async void Tentativa_De_Auth_Com_User_Invalido()
        {
            string endpoint = "/v1/GeoLog/auth";

            LoginRequest requestErrada = _testsFixture.GerarLoginInvalido();

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(requestErrada), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _testsFixture.Client.PostAsync(endpoint, httpContent);


            //eu quero que dê 404, já que o usuario nao existe
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
