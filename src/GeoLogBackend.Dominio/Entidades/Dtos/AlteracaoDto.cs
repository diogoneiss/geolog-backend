using GeoLogBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos
{
    public class AlteracaoDto
    {


        public AlteracaoDto()
        {

        }

        public AlteracaoDto(string usuario, string siglaPais, Guid idPais, Modificacao modificacaoFeita)
        {
            Usuario = usuario;
            SiglaPais = siglaPais;
            IdPais = idPais;
            ModificacaoFeita = modificacaoFeita;
        }
        public AlteracaoDto(Usuario usuario, string siglaPais, Guid idPais, Modificacao modificacaoFeita)
        {
            Usuario = usuario.Nome;
            SiglaPais = siglaPais;
            IdPais = idPais;
            ModificacaoFeita = modificacaoFeita;
        }

        public AlteracaoDto(Usuario usuario, Pais pais, Modificacao modificacaoFeita)
        {
            Usuario = usuario.Nome;
            SiglaPais = pais.IdSequencial.ISO31661_ALPHA2;
            IdPais = pais.Id;
            ModificacaoFeita = modificacaoFeita;
        }


        public AlteracaoDto(string usuario, Pais pais, Modificacao modificacaoFeita)
        {
            Usuario = usuario;
            SiglaPais = pais.IdSequencial.ISO31661_ALPHA2;
            IdPais = pais.Id;
            ModificacaoFeita = modificacaoFeita;
        }

        public string Usuario { get; set; }
        public string SiglaPais { get; set; }
        public Guid IdPais { get; set; }

        public Modificacao ModificacaoFeita { get; set; }
    }
}
