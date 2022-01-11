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
            .MaximumLength(3)
            .WithMessage("API so suporta siglas de paises, com no maximo 3 caracteres")
            .Must(p => !p.Any(char.IsDigit))
            .WithMessage("Pais nao deve conter numeros");
            }
        }
}
