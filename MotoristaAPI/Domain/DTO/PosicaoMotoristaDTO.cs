using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PosicaoMotoristaDTO
    {
        public Guid IdMotorista { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
