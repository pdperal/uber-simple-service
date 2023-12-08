using Domain.DTO;
using Domain.Entities;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IMotoristaService
    {
        public Result<NovoMotoristaViewModel> CadastrarMotorista(NovoMotoristaDTO motorista);
        public Result<List<MotoristaViewModel>> ListarMotoristas();
    }
}
