using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios
{
    public class PaisRepository : Repository<Pais>, IPaisRepository
    {
        private readonly DbSet<Pais> _paises;

        public PaisRepository(IMongoDatabase database, string collectionName)
           : base(database, collectionName)
        {
        }

        public async Task<IEnumerable<Pais>> FindPaisesBySigla(string sigla)
        {
            string siglaMaiuscula = sigla.ToUpper();
            var result = await Find(x => x.IdSequencial.ISO31661_ALPHA2 == siglaMaiuscula);
            if (result is null) {
                result = await Find(x => x.IdSequencial.ISO31661_ALPHA3 == siglaMaiuscula);

            }
            return result;
        }
    }
}
