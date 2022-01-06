using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;


namespace GeoLogBackend.Dominio
{
    public class UnidadeMonetaria : Entidade, IAggregateRoot
    {
        public string Nome { get; set; }
    }
}