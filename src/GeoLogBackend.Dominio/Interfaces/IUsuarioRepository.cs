using GeoLogBackend.Dominio;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<bool> validarLogin(Usuario usuario);

        public Task alterarSenha(string usuario);
    }
}
