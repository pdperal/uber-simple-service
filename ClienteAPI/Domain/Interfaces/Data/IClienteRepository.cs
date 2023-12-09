using Domain.DTO.Clientes;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data
{
    public interface IClienteRepository
    {
        public string BuscarIdClientePorCpf(string cpf);
        public bool SalvarCliente(Cliente cliente);
        public List<ClienteDTO> BuscarTodosClientes();
    }
}
