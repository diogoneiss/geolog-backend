using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos
{
    public class UsuarioDto
    {
        public UsuarioDto(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
