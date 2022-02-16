using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios
{
    public class LogRepository : Repository<LogAlteracao>, ILogRepository
    {
        private readonly DbSet<LogAlteracao> _alteracoes;

        public LogRepository(IMongoDatabase database, string collectionName)
           : base(database, collectionName)
        {
        }

        public async Task<IEnumerable<LogAlteracao>> AlteracoesPais(string sigla)
        {
            var nome = sigla.ToUpper();
            var result = await Find(x => x.PaisModificado.IdSequencial.ISO31661_ALPHA2 == nome);
            if(result is null)
                result = await Find(x => x.PaisModificado.IdSequencial.ISO31661_ALPHA3 == nome);

            return result;
        }

        public async Task<IEnumerable<LogAlteracao>> AlteracoesUsuario(string nome)
        {
            var result = await Find(x => x.UsuarioQueModificou == nome);

            return result;
        }

       
    }
}
