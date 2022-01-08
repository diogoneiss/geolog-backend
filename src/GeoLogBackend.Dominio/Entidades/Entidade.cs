using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GeoLogBackend.Dominio
{
    public abstract class Entidade
    {

        [BsonId]
        [BsonRepresentation(BsonType.String)] 
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }

}
