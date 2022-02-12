using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<bool> validarLogin(Usuario usuario);

        public Task alterarSenha(Usuario original, UsuarioDto usuario);

        public Task<Usuario> RecuperarUsuario(string nome);
       
    }
}
