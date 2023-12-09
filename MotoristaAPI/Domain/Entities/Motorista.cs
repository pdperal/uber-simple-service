using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Motorista
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public Motorista(string nome, string cPF)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CPF = cPF;
        }
    }
}
