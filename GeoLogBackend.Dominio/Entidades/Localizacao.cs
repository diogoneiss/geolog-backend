using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Localizacao : Entidade, IAggregateRoot
    {
        public Regiao Regiao { get; set; }
        public SubRegiao SubRegiao { get; set; }
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }
}