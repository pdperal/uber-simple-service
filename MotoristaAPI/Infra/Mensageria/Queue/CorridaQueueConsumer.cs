using Domain.Entities;
using Domain.Interfaces.Mensageria;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infra.Mensageria.Queue
{
    public class CorridaQueueConsumer : ICorridaQueueConsumer
    {
        private readonly IModel _channel;
        private readonly string _exchange = "nova-corrida-exchange";
        public CorridaQueueConsumer(IModel connection)
        {
            _channel = connection;
        }

        public async Task ConsumirMensagem(Action<Corrida> action)
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (sender, eventArgs) =>
                {
                    try
                    {
                        var contentByteArray = eventArgs.Body.ToArray();
                        var contentString = Encoding.UTF8.GetString(contentByteArray);
                        var message = JsonConvert.DeserializeObject<Corrida>(contentString);

                        action.Invoke(message);
                        _channel.BasicAck(eventArgs.DeliveryTag, false);
                    }
                    catch
                    {
                        _channel.BasicAck(eventArgs.DeliveryTag, false);
                    }

                };

                _channel.BasicConsume("nova-corrida-queue", false, consumer);
                await Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
    }
}
