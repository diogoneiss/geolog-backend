﻿using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;




namespace GeoLogBackend.Dominio
{
    public class SubRegiao : Entidade, IAggregateRoot
    {
        public new ID Id { get; set; }
        public string Nome { get; set; }
    }
}