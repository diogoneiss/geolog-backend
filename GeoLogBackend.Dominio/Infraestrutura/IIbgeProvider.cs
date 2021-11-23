using System.Threading.Tasks;

namespace GeoLogBackend.Dominio.Infraestrutura
{
    public interface IIbgeProvider
    {
        public Task<string> ObterPaisesIBGE(string paises);
    }
}
