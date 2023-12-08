using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.ViewModels;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Result<NovoClienteViewModel> CadastrarCliente(NovoClienteDTO cliente)
        {
            try
            {
                var entidade = cliente.ToEntity();

                var idClienteDb = _clienteRepository.BuscarIdClientePorCpf(entidade.CPF);

                if (!string.IsNullOrWhiteSpace(idClienteDb))
                {
                    return new Result<NovoClienteViewModel>
                        (
                            success: true,
                            message: "Cliente já cadastrado",
                            data: new NovoClienteViewModel(Guid.Parse(idClienteDb))
                        );                    
                }

                var sucesso = _clienteRepository.SalvarCliente(entidade);

                if (sucesso)
                {
                    return new Result<NovoClienteViewModel>
                        (
                            success: true,
                            data: new NovoClienteViewModel(entidade.Id)
                        );
                }

                return new Result<NovoClienteViewModel>
                   (
                       success: false,
                       message: "Não foi possível cadastrar o cliente."
                   );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new Result<NovoClienteViewModel>
                    (
                        success: false,
                        message: "Erro ao cadastrar novo cliente."
                    );
            }
        }

        public Result<List<ClienteViewModel>> ListarClientes()
        {
            try
            {
                var clientes = _clienteRepository.BuscarTodosClientes();

                if (clientes.Any())
                {
                    var clientesResponse = clientes
                        .Select(x => new ClienteViewModel(x.Id, x.Nome, x.CPF))
                        .ToList();

                    return new Result<List<ClienteViewModel>>(success: true, data: clientesResponse);
                }

                return new Result<List<ClienteViewModel>>(success: true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new Result<List<ClienteViewModel>>
                    (
                        success: false,
                        message: "Erro ao cadastrar novo cliente."
                    );
            }
        }
    }
}
