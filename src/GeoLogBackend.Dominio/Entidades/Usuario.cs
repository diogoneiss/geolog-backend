using GeoLogBackend.GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using System;

namespace GeoLogBackend.Dominio
{
    public class Usuario : Entidade, IAggregateRoot
    {
        public Usuario(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
            hashearSenha();
        }
        public Usuario(Guid id, DateTime instante, string nome, string senhaEmHash)
        {

            base.Id = id;
            base.CreatedAt = instante;
            Nome = nome;
            Senha = senhaEmHash;
            _jaHasheado = true;
        }
        private void hashearSenha()
        {
            if (_jaHasheado)
                return;
            Senha = SecurityService.CriarHash(Senha);
            _jaHasheado = true;
        }
 
        private bool _jaHasheado = false;

        public string Nome { get; set; }
        public string Senha
        {
            get; set;
        }
    }
}
