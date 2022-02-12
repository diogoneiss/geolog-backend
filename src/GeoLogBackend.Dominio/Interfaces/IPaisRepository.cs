using GeoLogBackend.Dominio;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    public interface IPaisRepository : IRepository<Pais>
    {
        Task<System.Collections.Generic.IEnumerable<Pais>> FindPaisesBySigla(string sigla);
    }
}
