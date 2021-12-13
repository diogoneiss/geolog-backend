using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLogBackend.Dominio.Interfaces
{
    public interface IIbgeProvider
    {
        public Task<List<Pais>> ObterPaisesIBGE(string paises);
    }
}
