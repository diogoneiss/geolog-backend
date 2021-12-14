using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios
{

    public abstract class Repository<T> : IRepository<T> where T : Entidade, IAggregateRoot
        {
        public readonly IMongoCollection<T> Collection;

        protected Repository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public Task Add(T instance) => Collection.InsertOneAsync(instance);

        public Task<T> FindFirst(Expression<Func<T, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public Task<T> FindById(Guid id) => Collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> Find() => await Collection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate) =>
            await Collection.Find(predicate).ToListAsync();

        public async Task<bool> Update(T instance)
        {
            ReplaceOneResult result = await Collection.ReplaceOneAsync(entity => entity.Id == instance.Id, instance);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Remove(T instance)
        {
            DeleteResult result = await Collection.DeleteOneAsync(entity => entity.Id == instance.Id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
