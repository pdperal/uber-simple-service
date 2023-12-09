using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Cotacao
{
    public class NovaCotacaoDTO
    {
        public Guid IdCliente { get; set; }
        public string LatitudeOrigem { get; set; }
        public string LongitudeOrigem { get; set; }
        public string LatitudeDestino { get; set; }
        public string LongitudeDestino { get; set; }
    }
}
