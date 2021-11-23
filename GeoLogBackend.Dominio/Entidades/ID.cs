namespace GeoLogBackend.Dominio
{
    public record ID
    {
        public ushort M49 { get; set; }
        public string ISO31661_ALPHA2 { get; set; }
        public string ISO31661_ALPHA3 { get; set; }

    }
}