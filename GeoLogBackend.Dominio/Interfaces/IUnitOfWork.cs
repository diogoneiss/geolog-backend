﻿namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
   public interface IUnitOfWork
    {
        IPaisRepository Paises { get;  }
    }
}
