using GeoLogBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    public interface IRepository<T> where T : Entidade, IAggregateRoot
    {
        Task Add(T instance);

        Task<T> FindById(Guid id);

        Task<IEnumerable<T>> Find();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<T> FindFirst(Expression<Func<T, bool>> predicate);

        Task<bool> Update(T instance);

        Task<bool> Remove(T instance);
    }
}
