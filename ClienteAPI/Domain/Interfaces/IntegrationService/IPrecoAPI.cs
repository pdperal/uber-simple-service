using Domain.DTO.Cotacao;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IntegrationService
{
    public interface IPrecoAPI
    {
        public CotacaoDTO ObterCotacaoCorrida(NovaCotacaoDTO novaCotacaoDTO);
    }
}
