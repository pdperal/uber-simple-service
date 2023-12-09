using Domain.DTO.Clientes;
using Domain.Entities;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Result<NovoClienteViewModel> CadastrarCliente(NovoClienteDTO cliente);
        public Result<List<ClienteViewModel>> ListarClientes();
    }
}
