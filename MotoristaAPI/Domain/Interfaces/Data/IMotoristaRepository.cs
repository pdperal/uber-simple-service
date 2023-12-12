using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data
{
    public interface IMotoristaRepository
    {
        public string BuscarIdMotoristaPorCpf(string cpf);
        public bool SalvarMotorista(Motorista motorista);
        public List<CorridaDTO> ListarCorridas();
        public bool FinalizarCorrida(Guid corridaId);
        public bool AssociarCorridaAoMotorista(CorridaMotorista corridaMotorista);
        public List<MotoristaDTO> BuscarTodosMotoristas();
    }
}
