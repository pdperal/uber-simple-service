using Domain.DTO.Cotacao;
using Domain.Entities;
using Domain.Interfaces.IntegrationService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infra.IntegrationService
{
    public class PrecoAPI : IPrecoAPI
    {
        private readonly HttpClient _httpClient;

        public PrecoAPI()
        {
            _httpClient = new HttpClient();
        }

        public Cotacao ObterCotacaoCorrida(NovaCotacaoDTO novaCotacaoDTO)
        {
            try
            {
                var request = _httpClient
                    .GetAsync($"https://localhost:7176/Preco/CalcularCotacao?latitudeOrigem={novaCotacaoDTO.LatitudeOrigem}" +
                    $"&longitudeOrigem={novaCotacaoDTO.LongitudeOrigem}&latitudeDestino={novaCotacaoDTO.LatitudeDestino}" +
                    $"&longitudeDestino={novaCotacaoDTO.LongitudeDestino}").GetAwaiter().GetResult();

                if (request.IsSuccessStatusCode)
                {
                    var cotacao = request.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<Cotacao>(cotacao);
                }

                return default;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
