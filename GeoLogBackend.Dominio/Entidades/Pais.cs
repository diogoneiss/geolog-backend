using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using System;

namespace GeoLogBackend.Dominio
{
    public class Pais : Entidade, IAggregateRoot
    {
        public Pais(ID id, Nome nome, Area area, Localizacao localizacao, Lingua[] linguas, Governo governo, UnidadeMonetaria[] moedas, string historico)
        {
            IdSequencial = id;
            Nome = nome;
            Area = area;
            Localizacao = localizacao;
            Linguas = linguas;
            Governo = governo;
            Moedas = moedas;
            Historico = historico;
        }

        public ID IdSequencial { get; set; }
        public Nome Nome { get; set; }
        public Area Area { get; set; }
        public Localizacao Localizacao { get; set; }
        public Lingua[] Linguas { get; set; }
        public Governo Governo { get; set; }
        public UnidadeMonetaria[] Moedas { get; set; }
        public string Historico { get; set; }
    }
}
