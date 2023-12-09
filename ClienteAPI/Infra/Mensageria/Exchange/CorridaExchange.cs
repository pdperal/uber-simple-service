using Domain.Interfaces.Mensageria;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infra.Mensageria.Exchange
{
    public class CorridaExchange : ICorridaExchange
    {
        private readonly IModel _channel;
        private readonly string _exchange = "nova-corrida-exchange";
        public CorridaExchange(IModel connection)
        {
            _channel = connection;
        }

        public void PublicarExchange(string message)
        {
            try
            {
                _channel.BasicPublish(_exchange, "nova-corrida", null, Encoding.UTF8.GetBytes(message));
            }
            catch
            {
                throw;
            }
        }
    }
}
