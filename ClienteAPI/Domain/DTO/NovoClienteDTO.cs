using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
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
