using GeoLogBackend.Dominio;
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

        public Task alterarSenha(string usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> validarLogin(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
