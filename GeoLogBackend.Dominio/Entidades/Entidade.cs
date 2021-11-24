using System;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeoLogBackend.Dominio
{
    public abstract class Entidade
    {

        
        [BsonRepresentation(BsonType.String)] public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }

}
