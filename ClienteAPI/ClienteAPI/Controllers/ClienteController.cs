using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpPost]
        [Route("CadastrarCliente")]
        public IActionResult CadastrarCliente(NovoClienteDTO novoClienteDTO)
        {
            var resultado = _clienteService.CadastrarCliente(novoClienteDTO);

            if (resultado.Success)
            {
                return Ok(resultado.Data.Id);
            }

            return BadRequest(resultado.Message);
        }

        [HttpGet]
        [Route("ListarClientes")]
        public IActionResult ListarClientes()
        {
            var resultado = _clienteService.ListarClientes();

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