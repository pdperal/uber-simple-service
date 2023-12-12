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

        public void GerarPosicaoAleatoria()
        {
            try
            {
                var motoristas = _motoristaRepository.BuscarTodosMotoristas();

                List<MotoristaCache> list = new();

                foreach(var mototista in motoristas)
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

                    list.Add(new MotoristaCache { Id = mototista.Id,
                        Latitude = latitudeParse, Longitude = longitudeParse
                    });
                }

                _redisService.SetCache("motoristas:posicoes", JsonConvert.SerializeObject(list));
            }
            catch
            {
                throw;
            }
        }    
        
        private static string GerarPosicaoAleatoria(int direcaoMax, int direcaoMin)
        {
            Random random = new Random();

            return random.Next(direcaoMin, direcaoMax).ToString().PadLeft(6, '0');
        }
    }
}
