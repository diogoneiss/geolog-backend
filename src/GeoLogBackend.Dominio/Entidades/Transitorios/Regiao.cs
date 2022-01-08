using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace GeoLogBackend.Dominio
{
    public class Regiao : IAggregateRoot
    {
        [BsonId]
        public new ID Id { get; set; }
        public string Nome { get; set; }
    }
}