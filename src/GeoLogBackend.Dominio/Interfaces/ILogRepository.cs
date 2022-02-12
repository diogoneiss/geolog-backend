using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    public interface ILogRepository : IRepository<LogAlteracao>
    {
        Task<System.Collections.Generic.IEnumerable<LogAlteracao>> AlteracoesPais(string sigla);
        Task<System.Collections.Generic.IEnumerable<LogAlteracao>> AlteracoesUsuario(string sigla);

    }
}

