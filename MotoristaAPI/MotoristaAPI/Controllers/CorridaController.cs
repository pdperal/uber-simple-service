using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MotoristaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorridaController : ControllerBase
    {
        private readonly ILogger<CorridaController> _logger;
        private readonly ICorridaService _corridaService;

        public CorridaController(ILogger<CorridaController> logger, ICorridaService corridaService)
        {
            _logger = logger;
            _corridaService = corridaService;
        }

        [HttpGet]
        [Route("ListarCorridas")]
        public IActionResult ListarCorridas()
        {
            var corridas = _corridaService.ListarCorridas();

            return Ok(corridas);
        }

        [HttpGet]
        [Route("FinalizarCorrida")]
        public IActionResult FinalizarCorrida(Guid idCorrida)
        {
            _corridaService.FinalizarCorrida(idCorrida);

            return Ok();
        }


    }
}