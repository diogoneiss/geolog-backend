
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class Governo : Entidade, IAggregateRoot
    {
        public string Capital { get; set; }
    }
}