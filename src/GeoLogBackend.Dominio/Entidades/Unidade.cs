using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Unidade : Entidade, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Simbolo { get; set; }
        public double Multiplicador { get; set; }
    }
}