using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Mensageria
{
    public interface ICorridaExchange
    {
        public void PublicarExchange(string message);
    }
}
