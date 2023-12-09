using Domain.DTO.Clientes;
using Domain.DTO.Corrida;
using Domain.Entities;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICorridaService
    {
        public void SolicitarCorrida(NovaCorridaDTO novaCorridaDTO);
    }
}
