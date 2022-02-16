using GeoLogBackend.Dominio;
using GeoLogBackend.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeoLogBackend.Infraestrutura.Http
{
    public class IbgeProvider : IIbgeProvider
    {
        private const string url = "https://servicodados.ibge.gov.br/api/v1/paises/";

        public async Task<List<Pais>> ObterPaisesIBGE(string paises)
        {
            HttpClient cliente = new();
            cliente.BaseAddress = new Uri(url);

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await cliente.GetAsync(paises);
            if (response.IsSuccessStatusCode)
            {
                var paisesDto = await response.Content.ReadAsAsync<List<Pais>>();

                //Se nao vazio e inexistente, criar vazio
                if(paises != "" && !paisesDto.Exists(x => x.IdSequencial.ISO31661_ALPHA2 == paises))
                {
                    paisesDto = new List<Pais>();
                    paisesDto.Add(new Pais(paises));
                }
                cliente.Dispose();
                return paisesDto;
            }
            else
            {
                cliente.Dispose();
                throw new HttpRequestException("Erro para obter dados de países do IBGE" + response.StatusCode.ToString() + response.ReasonPhrase);
            }
        }
    }
}
