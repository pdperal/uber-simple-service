using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Cache;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Newtonsoft.Json;

namespace Domain.Services
{
    public class CorridaService : ICorridaService
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IRedisService _redisService;

        public CorridaService(IMotoristaRepository motoristaRepository, IRedisService redisService)
        {
            _motoristaRepository = motoristaRepository;
            _redisService = redisService;
        }
        public void FinalizarCorrida(Guid idCorrida)
        {
            try
            {
                _motoristaRepository.FinalizarCorrida(idCorrida);
            }
            catch
            {
                throw;
            }
        }
        public List<CorridaDTO> ListarCorridas()
        {
            try
            {
                return _motoristaRepository.ListarCorridas();
            }
            catch
            {
                throw;
            }
        }

        public void ProcessarSolicitacaoCorrida(Corrida corrida)
        {
            try
            {
                var cotacaoCache = _redisService.GetCache($"cotacao:{corrida.IdCotacao}");

                if (string.IsNullOrWhiteSpace(cotacaoCache))
                {
                    throw new Exception("Cotacao nao encontrada");
                }

                var cotacao = JsonConvert.DeserializeObject<Cotacao>(cotacaoCache);

                var posicaoMotoristasCache = _redisService.GetCache("motoristas:posicoes");

                var posicoes = JsonConvert.DeserializeObject<List<MotoristaCache>>(posicaoMotoristasCache);

                List<MotoristaPosicaoOrigemDTO> lista = new();

                foreach(var posicao in posicoes)
                {
                    var posicaoMotorista = (Math.Abs(posicao.Longitude) - Math.Abs(cotacao.LocalizacaoOrigem.Longitude))
                        + (Math.Abs(posicao.Latitude) - Math.Abs(cotacao.LocalizacaoOrigem.Latitude));

                    lista.Add(new MotoristaPosicaoOrigemDTO { IdMotorista = posicao.Id, Posicao = posicaoMotorista });
                }

                var motoristaMaisPerto = lista.OrderBy(x => x.Posicao).First();

                _motoristaRepository.AssociarCorridaAoMotorista(new CorridaMotorista(corrida.IdCliente, corrida.IdCotacao, motoristaMaisPerto.IdMotorista));

            }
            catch
            {
                throw;
            }
            
        }
    }
}
