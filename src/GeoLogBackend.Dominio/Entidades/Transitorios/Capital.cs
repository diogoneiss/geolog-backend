using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Capital : IAggregateRoot
    {
        public string Nome { get; set; }
    }
}