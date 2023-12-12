using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cotacao
    {
        public Guid Id { get; set; }
        public decimal Preco { get; set; }
        public Localizacao LocalizacaoOrigem { get; set; }
        public Localizacao LocalizacaoDestino { get; set; }
    }
}
