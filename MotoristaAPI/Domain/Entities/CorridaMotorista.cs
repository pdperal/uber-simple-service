using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CorridaMotorista
    {
        public Guid IdCliente { get; set; }
        public Guid IdCotacao { get; set; }
        public Guid IdMotorista { get; set; }

        public CorridaMotorista(Guid idCliente, Guid idCotacao, Guid idMotorista)
        {
            IdCliente = idCliente;
            IdCotacao = idCotacao;
            IdMotorista = idMotorista;
        }
    }
}
