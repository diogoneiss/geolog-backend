using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class Lingua : Entidade, IAggregateRoot
    {
        public string Nome { get; set; }
    }
}