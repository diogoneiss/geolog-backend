using FluentValidation;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio
{
    public class PaisService : IPaisService
    {


        private readonly IPaisRepository _paisRepository;
        //private readonly IUnitOfWork _uow;

        public PaisService(IUnitOfWork uow)
                              
        {
            //_uow = uow;
            _paisRepository = uow.Paises;
        }



        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
                                                    where TV : AbstractValidator<TE>
                                                    where TE : IEntity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;


            return false;
        }
    }
}
