using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace GeoLogBackend.Dominio
{
    public class Pais : Entidade, IAggregateRoot
    {
        [JsonConstructor]
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

        public Pais(string sigla)
        {
            IdSequencial = new ID();
            IdSequencial.ISO31661_ALPHA2 = sigla;
            Nome = new Nome();
            Area = new Area();
            Localizacao = new Localizacao();
            Linguas = Array.Empty<Lingua>();
            Governo = new Governo();
            UnidadesMonetarias = Array.Empty<UnidadeMonetaria>();
            Historico = "";
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
                    if (Nome is null)
                    {
                        Nome = new Nome();
                    }
                    Nome.Abreviado = informacao.Valor.ToString();
                    break;
                case "Area":
                    if (Area is null)
                    {
                        Area = new Area();
                    }
                    Area.Total = informacao.Valor.ToString();
                    break;
                case "Regiao":
                    if (Localizacao.Regiao is null)
                    {
                        Localizacao.Regiao = new Regiao();
                    }
                    Localizacao.Regiao.Nome = informacao.Valor.ToString();
                    break;
                case "Sub-regiao":
                    if (Localizacao.SubRegiao is null)
                    {
                        Localizacao.SubRegiao = new SubRegiao();
                    }
                    Localizacao.SubRegiao.Nome = informacao.Valor.ToString();
                    break;
                case "Regiao-intermediaria":
                    if (Localizacao.RegiaoIntermediaria is null)
                    {
                        Localizacao.RegiaoIntermediaria = new RegiaoIntermediaria();
                    }
                    Localizacao.RegiaoIntermediaria.Nome = informacao.Valor.ToString();
                    break;
                case "Linguas":
                    if(Linguas is null)
                    {
                        Linguas = Array.Empty<Lingua>();
                    }
                    var linguas = Linguas.ToList();
                    linguas.Add(new Lingua
                    {
                        Nome = informacao.Valor.ToString()
                    });
                    Linguas = linguas.ToArray();
                    break;
                case "Governo":
                    if (Governo.Capital is null)
                    {
                        Governo.Capital = new Capital();
                    }
                    Governo.Capital.Nome = informacao.Valor.ToString();
                    break;
                case "Moeda":
                    if (UnidadesMonetarias is null)
                    {
                        UnidadesMonetarias = Array.Empty<UnidadeMonetaria>();
                    }
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
                    throw new ArgumentOutOfRangeException($"O campo {informacao.Campo} não foi encontrado");
            }
        }
    }
}
