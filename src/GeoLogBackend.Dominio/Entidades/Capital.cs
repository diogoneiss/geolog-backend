using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Capital : Entidade, IAggregateRoot
    {
        public string Nome { get; set; }
    }
}