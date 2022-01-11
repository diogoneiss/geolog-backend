using GeoLogBackend.Dominio;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Entidades
{
    public class PaisResponseDto : Entidade
    {

        public PaisResponseDto(Pais original)
        {
            Moedas = original.UnidadesMonetarias.Select(x => x.Nome).ToArray();
            Linguas = original.Linguas.Select(x => x.Nome).ToArray();
            Nome = original.Nome.Abreviado;
            IdSequencial = original.IdSequencial;
            Area = $"{original.Area.Total}{original.Area.Unidade.Simbolo}";
            Localizacao = original.Localizacao;

            Governo = original.Governo.Capital.Nome;
            Historico = original.Historico;
            this.Id = original.Id;
            this.CreatedAt = original.CreatedAt;
        }

        [BsonId]
        public Guid Id;
        public ID IdSequencial { get; set; }
        public DateTime CreatedAt;
     
        public String Nome { get; set; }
        public String[] Moedas { get; set; }
        


        
        public String Area { get; set; }
        public Localizacao Localizacao { get; set; }
        public String[] Linguas { get; set; }


        public String Governo { get; set; }
      
        public string Historico { get; set; }
    }
}
