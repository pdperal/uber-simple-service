using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Cotacao
{
    public class CotacaoDTO
    {
        public Guid Id { get; set; }
        public decimal Preco { get; set; }
    }
}
