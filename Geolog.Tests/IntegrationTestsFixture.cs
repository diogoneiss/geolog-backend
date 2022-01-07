using System;
using System.Net.Http;
using Bogus;
using Bogus.DataSets;
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
            Client = Factory.CreateClient();

        }

        /*
         * Exemplo de método que usarei no futuro
        public CreateAccountDto GerarCreateAccountDtoValido()
        {
            var faker = new Faker("pt_BR");

            var genero = faker.PickRandom<Name.Gender>();
            string ddd = "31";
            string numero = faker.Random.Number(900000000, 999999999).ToString();
            string nome = faker.Name.FirstName(genero);
            string sobrenome = faker.Name.LastName(genero);
            var clienteValido = new Faker<CreateAccountDto>("pt_BR")
                .CustomInstantiator(f => new CreateAccountDto(
                    nome,
                    sobrenome,
                    "11600297684",
                    ddd + numero,
                    f.Internet.ExampleEmail(nome, sobrenome)

                ));


            return clienteValido;
        }
        */


        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}
