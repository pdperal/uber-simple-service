using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class MotoristaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}
