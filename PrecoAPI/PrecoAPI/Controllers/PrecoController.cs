using Domain.Interfaces;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocalizacaoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecoController : ControllerBase
    {
        private readonly ILogger<PrecoController> _logger;
        private readonly IPrecoService _geoLocalizacaoService;

        public PrecoController(ILogger<PrecoController> logger, IPrecoService geoLocalizacaoService)
        {
            _logger = logger;
            _geoLocalizacaoService = geoLocalizacaoService;
        }

        [HttpGet]
        [Route("CalcularCotacao")]
        public IActionResult CalcularCotacao(string latitudeOrigem, string longitudeOrigem, string latitudeDestino, string longitudeDestino)
        {

            var result = _geoLocalizacaoService.CalcularCotacao(latitudeOrigem, longitudeOrigem, latitudeDestino, longitudeDestino);
            
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest();
            
        }
    }
}