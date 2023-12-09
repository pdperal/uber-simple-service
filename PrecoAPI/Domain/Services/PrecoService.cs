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
        public Result CalcularCotacao(string latitudeOrigem, string longitudeOrigem, string latitudeDestino, string longitudeDestino)
        {
            try
            {
                var latitudeOrigemDecimal = ConverterCoordenadas(latitudeOrigem);
                var longitudeOrigemDecimal = ConverterCoordenadas(longitudeOrigem);
                var latitudeDestinoDecimal = ConverterCoordenadas(latitudeDestino);
                var longitudeDestinoDecimal = ConverterCoordenadas(longitudeDestino);
                var chaveGoogle = File.ReadAllText("C:\\Users\\pep90\\Downloads\\chave_google.txt");

                var chamadaApi = _httpclient
                    .GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={latitudeDestinoDecimal.ToString("0.0000").Replace(',', '.')}%2C{longitudeDestinoDecimal.ToString("0.0000").Replace(',', '.')}&origins={latitudeOrigemDecimal.ToString("0.0000").Replace(',', '.')}%2C{longitudeOrigemDecimal.ToString("0.0000").Replace(',', '.')}&key={chaveGoogle}&mode=driving")
                    .GetAwaiter()
                    .GetResult();

                if (chamadaApi.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Rootobject>(chamadaApi.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                    var cotacao = new Result(CalcularValorCotacao(result.rows.First().elements.First().distance.value));

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

    public class Result
    {
        public Guid Id { get; private set; }
        public decimal Valor { get; private set; }

        public Result(decimal valor)
        {
            Id = Guid.NewGuid();
            Valor = valor;
        }
    }



    public class Rootobject
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}
