using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class Lingua :  IAggregateRoot
    {
        public string Nome { get; set; }
    }
}