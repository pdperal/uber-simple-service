using Domain.DTO;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PrecoService : IPrecoService
    {
        private readonly HttpClient _httpclient;
        private readonly IRedisService _redisService;

        public PrecoService(IRedisService redisService)
        {
            _httpclient = new HttpClient();
            _redisService = redisService;
        }
        public Cotacao CalcularCotacao(string latitudeOrigem, string longitudeOrigem, string latitudeDestino, string longitudeDestino)
        {
            try
            {
                var latitudeOrigemDecimal = ConverterCoordenadas(latitudeOrigem).ToString("0.0000");
                var longitudeOrigemDecimal = ConverterCoordenadas(longitudeOrigem).ToString("0.0000");
                var latitudeDestinoDecimal = ConverterCoordenadas(latitudeDestino).ToString("0.0000");
                var longitudeDestinoDecimal = ConverterCoordenadas(longitudeDestino).ToString("0.0000");
                var chaveGoogle = File.ReadAllText("C:\\Users\\pep90\\Downloads\\chave_google.txt");

                var chamadaApi = _httpclient
                    .GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={latitudeDestinoDecimal.Replace(',', '.')}%2C{longitudeDestinoDecimal.Replace(',', '.')}&origins={latitudeOrigemDecimal.Replace(',', '.')}%2C{longitudeOrigemDecimal.Replace(',', '.')}&key={chaveGoogle}&mode=driving")
                    .GetAwaiter()
                    .GetResult();

                if (chamadaApi.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<GoogleMapsResponseDto>(chamadaApi.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                    var preço = CalcularValorCotacao(result.rows.First().elements.First().distance.value);
                    var cotacao = new Cotacao(preço,
                        localizacaoOrigem: new Localizacao(double.Parse(latitudeOrigemDecimal), double.Parse(longitudeOrigemDecimal)),
                        localizacaoDestino: new Localizacao(double.Parse(latitudeDestinoDecimal), double.Parse(longitudeDestinoDecimal)));

                    _redisService.SetCache(cotacao.Id.ToString(), JsonConvert.SerializeObject(cotacao));

                    return cotacao;
                }

                return default;

            }
            catch
            {
                throw;
            }
        }

        private static decimal CalcularValorCotacao(int distanciaEmMetros)
        {
            // R$1.2 p/ KM fixo
            // < 10000 metros => taxa de 10%
            // => 10000 < 15000 metros => taxa de 12%
            // => 150000 < 20000 metros => taxa de 13%
            // => 20000 metros => taxa de 15%

            return distanciaEmMetros switch
            {
                < 10000 => ((distanciaEmMetros / 1000m) * 1.2m) * 1.1m,
                var x when x >= 10000 && x < 15000 => ((distanciaEmMetros / 1000m) * 1.2m) * 1.12m,
                var x when x >= 15000 && x < 20000 => ((distanciaEmMetros / 1000m) * 1.2m) * 1.13m,
                > 20000 => ((distanciaEmMetros / 1000m) * 1.2m) * 1.15m,
            };
        }

        private static double ConverterCoordenadas(string coordenadas)
        {
            var coordenadasSplit = coordenadas.Split('-');
            var grau = double.Parse(coordenadasSplit[0]);
            var minutos = double.Parse(coordenadasSplit[1]) / 60;
            double horas = double.Parse(coordenadasSplit[2], new CultureInfo("en-US")) / 3600;

            return -(grau + minutos + horas);
        }
    }
}
