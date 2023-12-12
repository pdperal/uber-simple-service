using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public Cliente(string nome, string cPF )
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CPF = cPF;
        }
    }
}
