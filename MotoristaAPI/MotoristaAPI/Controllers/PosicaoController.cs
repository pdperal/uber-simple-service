using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MotoristaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosicaoController : ControllerBase
    {
        private readonly ILogger<PosicaoController> _logger;
        private readonly IMotoristaService _motoristaService;

        public PosicaoController(ILogger<PosicaoController> logger, IMotoristaService motoristaService)
        {
            _logger = logger;
            _motoristaService = motoristaService;
        }

        [HttpPost]
        [Route("GerarPosicaoAleatoria")]
        public IActionResult CadastrarMotorista(Guid motoristaId)
        {
            var resultado = _motoristaService.CadastrarMotorista(novoMotoristaDTO);

            if (resultado.Success)
            {
                return Ok(resultado.Data.Id);
            }

            return BadRequest(resultado.Message);
        }

        [HttpGet]
        [Route("ListarMotorista")]
        public IActionResult ListarListarMotoristas()
        {
            var resultado = _motoristaService.ListarMotoristas();

            if (resultado.Success)
            {
                if (resultado.Data is not null
                     && resultado.Data.Any())
                {
                    return Ok(resultado.Data);
                }

                return NoContent();
                
            }

            return BadRequest(resultado.Message);
        }
    }
}