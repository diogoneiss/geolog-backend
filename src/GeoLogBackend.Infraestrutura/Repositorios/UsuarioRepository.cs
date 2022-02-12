using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly DbSet<Usuario> _usuarios;

    public UsuarioRepository(IMongoDatabase database, string collectionName)
       : base(database, collectionName)
        {}

        public async Task<Usuario> RecuperarUsuario(string nome)
        {
            var usuario = await FindFirst(x => x.Nome == nome);

            return usuario;
        }

        public async Task alterarSenha(Usuario original, UsuarioDto usuario)
        {
            Usuario atualizado = new Usuario(original.Nome, usuario.Senha);

            //re-atualizar o id antigo e data de criação, são modificados no construtor
            atualizado.Id = original.Id;
            atualizado.CreatedAt = original.CreatedAt;

            await Update(atualizado);
        }

        public Task<bool> validarLogin(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
