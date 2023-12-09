using Domain.DTO;
using Domain.DTO.Cotacao;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoController : ControllerBase
    {
        private readonly ILogger<CotacaoController> _logger;
        private readonly ICotacaoService _cotacaoService;
        private readonly IClienteService _clienteService;

        public CotacaoController(ILogger<CotacaoController> logger, ICotacaoService cotacaoService)
        {
            _logger = logger;
            _cotacaoService = cotacaoService;
        }

        [HttpGet]
        [Route("SolicitarCotacao")]
        public IActionResult SolicitarCotacao(Guid clientId, string latitudeOrigem, string longitudeOrigem, string latitudeDesino, string longitudeDestino)
        {

            var cotacao = _cotacaoService.SolicitarCotacao(new NovaCotacaoDTO
            {
                IdCliente = clientId,
                LatitudeDestino = latitudeDesino,
                LongitudeDestino = longitudeDestino,
                LatitudeOrigem = latitudeOrigem,
                LongitudeOrigem = longitudeOrigem,
            });

            if (cotacao.Success)
            {
                return Ok(cotacao.Data);
            }

            return BadRequest(cotacao.Message);
        }

    }
}