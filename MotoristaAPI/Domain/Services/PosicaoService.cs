using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Cache;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Newtonsoft.Json;

namespace Domain.Services
{
    public class PosicaoService : IPosicaoService
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IRedisService _redisService;

        public PosicaoService(IMotoristaRepository motoristaRepository, IRedisService redisService)
        {
            _motoristaRepository = motoristaRepository;
            _redisService = redisService;
        }      

        public void GerarPosicaoAleatoria(Guid motoristaId)
        {
            try
            {
                // método resonsavel por gerar uma posição aleatória dentro de Porto alegre
                // foi definido um quadrado no centro da cidade com as posições máximas (direita, esquerda, baixo, cima)
                // exemplo:
                //  ______
                // |      |
                // |      |
                // |      |
                // --------
                // o método pega esses valores min/max e gera um numero aleatorio entre eles, para determinar uma posição aleatoria dentro do quadrado

                var latitude = GerarPosicaoAleatoria(Constants.BAIXO, Constants.CIMA);
                var longitude = GerarPosicaoAleatoria(Constants.ESQUERDA, Constants.DIREITA);

                var latitudeParse = double.Parse(string.Concat(-30, ',', latitude));
                var longitudeParse = double.Parse(string.Concat(-51, ',', longitude));

                _redisService.SetCache(motoristaId.ToString(), JsonConvert.SerializeObject(new PosicaoMotoristaDTO
                {
                    IdMotorista = motoristaId,
                    Latitude = latitudeParse,
                    Longitude = longitudeParse
                }));


            }
            catch
            {
                throw;
            }
        }    
        
        private static string GerarPosicaoAleatoria(int direcaoMax, int direcaoMin)
        {
            Random random = new Random();
            //return random.NextDouble() * (direcaoMax - direcaoMin) + direcaoMin;

            return random.Next(direcaoMin, direcaoMax).ToString().PadLeft(6, '0');
        }
    }
}
