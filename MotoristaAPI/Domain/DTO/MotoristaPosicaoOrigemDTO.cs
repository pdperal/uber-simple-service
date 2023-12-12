using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class MotoristaPosicaoOrigemDTO
    {
        public Guid IdMotorista { get; set; }
        public double Posicao { get; set; }

    }
}
