using GeoLogBackend.GeoLogBackend.Api.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos
{
    public class PaisGetDto
    {

        public PaisGetDto()
        {

        }

        public PaisGetDto(string nome)
        {
            
            Nome = nome;
            
        }

        public PaisGetDto(string nome, ModelStateDictionary modelState) : this(nome)
        {
            var validacao = validation.Validate(this);
            if (!validacao.IsValid)
            {
                foreach(var erro in validacao.Errors.ToArray())
                {
                    modelState.AddModelError(Nome, erro.ToString());
                }
               
            }
        }

        private readonly GetPaisDtoValidation validation = new GetPaisDtoValidation();

        public String Nome { get; set; }
    }
}
