using FluentValidation;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Validations
{
    
    
        public class GetPaisDtoValidation : AbstractValidator<PaisGetDto>
        {
            public GetPaisDtoValidation()
            {
            //nao pode ser menor que 3 e nem conter numeros
            RuleFor(p => p.Nome)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .Must(p => !p.Any(char.IsDigit))
            .WithMessage("Pais nao deve conter numeros");
            }
        }
}
