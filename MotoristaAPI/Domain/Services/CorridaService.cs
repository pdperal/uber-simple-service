using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.ViewModels;

namespace Domain.Services
{
    public class CorridaService : ICorridaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public CorridaService(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }


        public void ProcessarSolicitacaoCorrida(Corrida corrida)
        {
            try
            {

            }
            catch
            {
                throw;
            }
            
        }
    }
}
