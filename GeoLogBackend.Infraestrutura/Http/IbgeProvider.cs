using GeoLogBackend.Dominio.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeoLogBackend.Infraestrutura.Http
{
    public class IbgeProvider : IIbgeProvider
    {
        private const string url = "https://servicodados.ibge.gov.br/api/v1/paises/";

        public async Task<string> ObterPaisesIBGE(string paises)
        {
            HttpClient cliente = new();
            cliente.BaseAddress = new Uri(url);

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await cliente.GetAsync(paises);
            if (response.IsSuccessStatusCode)
            {
                var paisesDto = await response.Content.ReadAsStringAsync();

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
