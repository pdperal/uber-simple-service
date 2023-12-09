

using Domain.DTO.Clientes;
using Domain.DTO.Corrida;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Services
{
    public class CorridaService : ICorridaService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICorridaExchange _corridaExchange;

        public CorridaService(IClienteRepository clienteRepository, ICorridaExchange corridaExchange)
        {
            _clienteRepository = clienteRepository;
            _corridaExchange = corridaExchange;
        }        

        public void SolicitarCorrida(NovaCorridaDTO novaCorridaDTO)
        {
            try
            {
                var message = JsonConvert.SerializeObject(novaCorridaDTO, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                _corridaExchange.PublicarExchange(message);
            }
            catch
            {
                throw;
            }
        }
    }
}
