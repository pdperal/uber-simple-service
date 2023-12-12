using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CorridaDTO
    {
        public Guid IdCorrida { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdMotorista { get; set; }
        public bool Finalizado { get; set; }
    }
}
