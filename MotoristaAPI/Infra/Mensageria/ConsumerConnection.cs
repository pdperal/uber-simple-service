using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mensageria
{
    public class ConsumerConnection
    {
        public IConnection Connection { get; private set; }

        public ConsumerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}
