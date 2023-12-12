using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cotacao
    {
        public Guid Id { get; private set; }
        public decimal Preco { get; private set; }

        public Cotacao(decimal valor)
        {
            Id = Guid.NewGuid();
            Preco = valor;
        }
    }
}
