using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Linq;

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

        public void ValidaAtualizacao(InformacaoPaisDto informacao)
        {
            string campo = informacao.Campo;

            switch (campo)
            {
                case "Nome":
                    Nome.Abreviado = informacao.Valor.ToString();
                    break;
                case "Area":
                    Area.Total = informacao.Valor.ToString();
                    break;
                case "Regiao":
                    Localizacao.Regiao.Nome = informacao.Valor.ToString();
                    break;
                case "Sub-regiao":
                    Localizacao.SubRegiao.Nome = informacao.Valor.ToString();
                    break;
                case "Regiao-intermediaria":
                    Localizacao.RegiaoIntermediaria.Nome = informacao.Valor.ToString();
                    break;
                case "Linguas":
                    var linguas = Linguas.ToList();
                    linguas.Add(new Lingua
                    {
                        Nome = informacao.Valor.ToString()
                    });
                    Linguas = linguas.ToArray();
                    break;
                case "Governo":
                    Governo.Capital.Nome = informacao.Valor.ToString();
                    break;
                case "Moeda":
                    var moedas = UnidadesMonetarias.ToList();
                    moedas.Add(new UnidadeMonetaria
                    {
                        Nome = informacao.Valor.ToString()
                    });
                    UnidadesMonetarias = moedas.ToArray();
                    break;
                case "Historico":
                    Historico = informacao.Valor.ToString();
                    break;
                default:
                    throw new System.Exception("O campo informado não foi encontrado");
            }
        }
    }
}
