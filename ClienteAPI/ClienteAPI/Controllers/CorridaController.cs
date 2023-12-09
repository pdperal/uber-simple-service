using Domain.DTO;
using Domain.DTO.Corrida;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.Controllers
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

        [HttpPost]
        [Route("SolicitarCorrida")]
        public IActionResult SolicitarCorrida(NovaCorridaDTO novaCorridaDTO)
        {
            _corridaService.SolicitarCorrida(novaCorridaDTO);

            return Ok();
        }       
    }
}