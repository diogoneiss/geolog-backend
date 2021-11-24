namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
    interface IUnitOfWork
    {
        IPaisRepository Paises { get;  }
    }
}
