using Newtonsoft.Json;

namespace GeoLogBackend.Dominio
{
    public record ID 
    {
        public ushort M49 { get; set; }
        
        [JsonProperty("ISO-3166-1-ALPHA-2")]
        public string ISO31661_ALPHA2 { get; set; }
        [JsonProperty("ISO-3166-1-ALPHA-3")]
        public string ISO31661_ALPHA3 { get; set; }
        [JsonProperty("ISO-639-1")]
        public string ISO6391 { get; set; }
        [JsonProperty("ISO-639-2")]
        public string ISO6392 { get; set; }
}
}