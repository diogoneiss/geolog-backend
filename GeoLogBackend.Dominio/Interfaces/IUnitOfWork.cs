using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Interfaces
{
   public interface IUnitOfWork
    {
        IPaisRepository Paises { get;  }
    }
}
