using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura
{
    public class UnitOfWork: IUnitOfWork
    {

        private IPaisRepository _paisRepository;
        private IUsuarioRepository _usuarioRepository;

        //se o privado for nulo, cria e atribui com construtor
        public IPaisRepository Paises =>
            _paisRepository ??= new PaisRepository(_database, "paises");

        public IUsuarioRepository Usuarios =>
          _usuarioRepository ??= new UsuarioRepository(_database, "users");


        private readonly IMongoDatabase _database;


        public UnitOfWork(IConfiguration configuration)
        {

            //user: admin
            //password: admin
            const string username = "admin";
            const string password = "admin";

            const string databaseName = "geolog";


            string CONNECTION_STRING = $"mongodb+srv://{username}:{password}@cluster0.5t0zc.mongodb.net/{databaseName}?retryWrites=true&w=majority";

            var settings = MongoClientSettings.FromConnectionString(CONNECTION_STRING);
            var client = new MongoClient(settings);
            _database = client.GetDatabase(databaseName);
        }

    }
}
