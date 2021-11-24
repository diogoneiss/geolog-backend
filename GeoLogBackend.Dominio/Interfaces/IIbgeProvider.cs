using System.Threading.Tasks;

namespace GeoLogBackend.Dominio.Interfaces
{
    public interface IIbgeProvider
    {
        public Task<string> ObterPaisesIBGE(string paises);
    }
}
