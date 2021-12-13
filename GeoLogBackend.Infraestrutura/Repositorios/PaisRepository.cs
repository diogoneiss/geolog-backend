using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios
{
    public class PaisRepository : Repository<Pais>, IPaisRepository
    {
        private readonly DbSet<Pais> _paises;

        public PaisRepository(IMongoDatabase database, string collectionName)
           : base(database, collectionName)
        {
        }
    }
}
