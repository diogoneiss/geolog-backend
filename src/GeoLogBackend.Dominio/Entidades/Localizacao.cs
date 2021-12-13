using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;



namespace GeoLogBackend.Dominio
{
    public class Localizacao : Entidade, IAggregateRoot
    {
        public Regiao Regiao { get; set; }
        public SubRegiao Sub_regiao { get; set; }
        public RegiaoIntermediaria Regiao_intermediaria { get; set; }
    }
}