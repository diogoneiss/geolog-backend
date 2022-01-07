using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Newtonsoft.Json;

namespace GeoLogBackend.Dominio
{
    public class Localizacao : Entidade, IAggregateRoot
    {
        public Regiao Regiao { get; set; }
        [JsonProperty("sub-regiao")]
        public SubRegiao SubRegiao { get; set; }
        [JsonProperty("regiao-intermediaria")]
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }
}