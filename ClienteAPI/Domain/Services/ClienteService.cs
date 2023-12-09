

using Domain.DTO.Clientes;
using Domain.DTO.Cotacao;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.IntegrationService;
using Domain.Interfaces.Services;
using Domain.ViewModels;

namespace Domain.Services
{
    public class CotacaoService : ICotacaoService
    {
        private readonly IPrecoAPI _precoApi;

        public CotacaoService(IPrecoAPI precoApi)
        {
            _precoApi = precoApi;
        }

        public Result<Cotacao> SolicitarCotacao(NovaCotacaoDTO novaCotacaoDTO)
        {
            try
            {
                var cotacao = _precoApi.ObterCotacaoCorrida(novaCotacaoDTO);

                if (cotacao is not null)
                {
                    return new Result<Cotacao>(success: true,
                        data: cotacao);
                }

                return new Result<Cotacao>(success: false,
                        message: "Não foi possível solicitar a cotação.");

            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
