using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Nome :  IAggregateRoot
    {
        public string Abreviado { get; set; }
    }
}