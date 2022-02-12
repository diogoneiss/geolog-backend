using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using GeoLogBackend.GeoLogBackend.Api.Configurations;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace GeoLogBackend.Api.Tests
{

    [CollectionDefinition(nameof(IntegrationApiFixtureCollection))]
    public class IntegrationApiFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTest>>
    {

    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public HttpClient Client;

        public readonly PaisFactory<TStartup> Factory;


        public IntegrationTestsFixture()
        {
            //Antigamente eu configurava o cliente logo abaixo aqui, porém por algum motivo isso não funciona mais?
            //o edu fez assim https://desenvolvedor.io/curso-online-dominando-os-testes-de-software/aula/5360cec7-a26e-4c27-96ca-a756ede43983#status
            Factory = new PaisFactory<TStartup>();

            //adicionando a autenticacao custom pra sobreescrever a normal
            Client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    /* Se desejar mockar as requests, tenho pronto.
                     
                    services.AddAuthentication("Bearer")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Bearer", options => { });
                    */
                    services.ConfigureSecurity();
                });
            })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
            
            //nao quero redirects
            AllowAutoRedirect = false,
            
        });

            //Colocar o padrão de header como Test no Schema
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer");
          

        }

        
        
        public LoginRequest GerarLoginInvalido()
        {
            var faker = new Faker("pt_BR");

            var genero = faker.PickRandom<Name.Gender>();
      
            string nome = faker.Name.FirstName(genero);
            string sobrenome = faker.Name.LastName(genero);
            string senha = faker.Internet.Password(length: 50);

            var usuarioTeste = new Faker<LoginRequest>("pt_BR")
                .CustomInstantiator(f => new LoginRequest(
                    f.Internet.ExampleEmail(nome, sobrenome),
                    senha
                ));


            return usuarioTeste;
        }

        public async Task<LoginRequest> GerarLoginValido()
        {

            //vou ver se o usuario está no banco 
            LoginRequest usuarioTeste = new LoginRequest("teste", "teste");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(usuarioTeste), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PostAsync("/v1/GeoLog/auth", httpContent);

            //nao encontrou? Sem problema, crio na mao
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                //criar usuario com os valores
                await Client.PostAsync("/v1/GeoLog/Usuarios", httpContent);
                response = await Client.PostAsync("/v1/GeoLog/auth", httpContent);
            }

            if ((int)response.StatusCode == StatusCodes.Status200OK)
            {
                return usuarioTeste;
            }

            throw new HttpRequestException("Usuario nao foi criado no banco");
        }



        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}
