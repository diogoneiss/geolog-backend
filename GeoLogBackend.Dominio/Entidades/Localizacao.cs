namespace GeoLogBackend.Dominio
{
    public class Localizacao
    {
        public Regiao Regiao { get; set; }
        public SubRegiao SubRegiao { get; set; }
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }
}