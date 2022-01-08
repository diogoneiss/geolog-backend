using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class UnidadeMonetaria : IAggregateRoot
    {
        public string Nome { get; set; }
    }
}