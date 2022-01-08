using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace GeoLogBackend.Dominio
{
    public class RegiaoIntermediaria :  IAggregateRoot 
    {
        [BsonId]
        public new ID Id { get; set; }
        public string Nome { get; set; }
    }
}