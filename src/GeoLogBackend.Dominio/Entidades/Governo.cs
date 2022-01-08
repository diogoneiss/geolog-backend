
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class Governo :  IAggregateRoot
    {
        public Capital Capital { get; set; }
    }
}