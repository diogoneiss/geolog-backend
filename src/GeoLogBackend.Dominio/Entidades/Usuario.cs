using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;

namespace GeoLogBackend.Dominio
{
    public class Usuario : Entidade, IAggregateRoot
    {
        public Usuario(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
