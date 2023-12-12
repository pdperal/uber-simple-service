using Domain.Entities;

namespace Domain.DTO.Clientes
{
    public class NovoClienteDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public Cliente ToEntity()
        {
            return new Cliente(Nome, CPF);
        }
    }
}
