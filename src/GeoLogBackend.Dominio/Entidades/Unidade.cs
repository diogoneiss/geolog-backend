using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using Newtonsoft.Json;

namespace GeoLogBackend.Dominio
{
    public class Unidade :  IAggregateRoot
    {
        public string Nome { get; set; }
        [JsonProperty("simbolo")]
        public string Simbolo { get; set; }
        public double Multiplicador { get; set; }
    }
}