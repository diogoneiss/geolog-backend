using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;

namespace GeoLogBackend.Dominio
{
    public class Area: IAggregateRoot
    {
        public string Total { get; set; }
        public Unidade Unidade { get; set; }
    }
}