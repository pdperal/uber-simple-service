using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Mensageria
{
    public interface ICorridaQueueConsumer
    {
        public Task ConsumirMensagem(Action<Corrida> func);
    }
}
