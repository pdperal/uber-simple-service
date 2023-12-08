using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class NovoMotoristaDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public Motorista ToEntity()
        {
            return new Motorista(Nome, CPF);
        }
    }
}
