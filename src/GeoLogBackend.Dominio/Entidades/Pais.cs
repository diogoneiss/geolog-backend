using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace GeoLogBackend.Dominio
{
    public class Pais : Entidade, IAggregateRoot
    {
        public Pais(ID id, Nome nome, Area area, Localizacao localizacao, Lingua[] linguas, Governo governo, UnidadeMonetaria[] unidadesMonetarias, string historico)
        {
            IdSequencial = id;
            Nome = nome;
            Area = area;
            Localizacao = localizacao;
            Linguas = linguas;
            Governo = governo;
            UnidadesMonetarias = unidadesMonetarias;
            Historico = historico;
        }

        //nao preciso do id pq ja herdo de entidade

        public ID IdSequencial { get; set; }
        public Nome Nome { get; set; }
        public Area Area { get; set; }
        public Localizacao Localizacao { get; set; }
        public Lingua[] Linguas { get; set; }
        public Governo Governo { get; set; }
        [JsonProperty("unidades-monetarias")]
        public UnidadeMonetaria[] UnidadesMonetarias { get; set; }
        public string Historico { get; set; }
    }
}
