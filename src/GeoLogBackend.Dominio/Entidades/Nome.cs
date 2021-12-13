using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Nome : Entidade, IAggregateRoot
    {
        public string Abreviado { get; set; }
    }
}