using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MotoristaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosicaoController : ControllerBase
    {
        private readonly ILogger<PosicaoController> _logger;
        private readonly IPosicaoService _posicaoService;

        public PosicaoController(ILogger<PosicaoController> logger, IPosicaoService posicaoService)
        {
            _logger = logger;
            _posicaoService = posicaoService;
        }

        [HttpPost]
        [Route("GerarPosicaoAleatoria")]
        public IActionResult CadastrarMotorista()
        {
            _posicaoService.GerarPosicaoAleatoria();           

            return Ok();
        }

        
    }
}