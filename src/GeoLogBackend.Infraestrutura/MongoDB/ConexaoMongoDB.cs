using MongoDB.Driver;

namespace GeoLogBackend.Infrastructure.MongoDB
{
    public class ConexaoMongoDB
    {
        private const string CONNECTION_STRING = "StringDeConexãoComOBanco";

        private readonly MongoClient cliente;

        public ConexaoMongoDB()
        {
            cliente = new (CONNECTION_STRING);
        }


    }
}
