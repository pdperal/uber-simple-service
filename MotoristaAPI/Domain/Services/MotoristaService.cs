using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.ViewModels;

namespace Domain.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }

        public Result<NovoMotoristaViewModel> CadastrarMotorista(NovoMotoristaDTO motorista)
        {
            try
            {
                var entidade = motorista.ToEntity();

                var idMotoristaDb = _motoristaRepository.BuscarIdMotoristaPorCpf(entidade.CPF);

                if (!string.IsNullOrWhiteSpace(idMotoristaDb))
                {
                    return new Result<NovoMotoristaViewModel>
                        (
                            success: true,
                            message: "Motorista já cadastrado",
                            data: new NovoMotoristaViewModel(Guid.Parse(idMotoristaDb))
                        );                    
                }

                var sucesso = _motoristaRepository.SalvarMotorista(entidade);

                if (sucesso)
                {
                    return new Result<NovoMotoristaViewModel>
                        (
                            success: true,
                            data: new NovoMotoristaViewModel(entidade.Id)
                        );
                }

                return new Result<NovoMotoristaViewModel>
                   (
                       success: false,
                       message: "Não foi possível cadastrar o motorista."
                   );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new Result<NovoMotoristaViewModel>
                    (
                        success: false,
                        message: "Erro ao cadastrar novo motorista."
                    );
            }
        }

        public Result<List<MotoristaViewModel>> ListarMotoristas()
        {
            try
            {
                var motoristas = _motoristaRepository.BuscarTodosMotoristas();

                if (motoristas.Any())
                {
                    var motoristasResponse = motoristas
                        .Select(x => new MotoristaViewModel(x.Id, x.Nome, x.CPF))
                        .ToList();

                    return new Result<List<MotoristaViewModel>>(success: true, data: motoristasResponse);
                }

                return new Result<List<MotoristaViewModel>>(success: true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new Result<List<MotoristaViewModel>>
                    (
                        success: false,
                        message: "Erro ao cadastrar novo motorista."
                    );
            }
        }
    }
}
