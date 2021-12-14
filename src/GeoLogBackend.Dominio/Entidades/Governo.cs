
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class Governo : Entidade, IAggregateRoot
    {
        public Capital Capital { get; set; }
    }
}