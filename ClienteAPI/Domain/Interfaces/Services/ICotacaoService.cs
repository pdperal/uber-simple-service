using Domain.DTO.Clientes;
using Domain.DTO.Cotacao;
using Domain.Entities;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICotacaoService
    {
        public Result<CotacaoDTO> SolicitarCotacao(NovaCotacaoDTO novaCotacaoDTO);
    }
}
