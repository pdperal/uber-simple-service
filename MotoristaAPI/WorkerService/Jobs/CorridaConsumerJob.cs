using Domain.Entities;
using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace WorkerService.Jobs;

public class CorridaConsumerJob : BackgroundService
{
    private readonly ICorridaQueueConsumer _corridaQueueConsumer;
    private readonly ICorridaService _corridaService;
    private readonly IModel _channel;

    public CorridaConsumerJob(ICorridaQueueConsumer corridaQueueConsumer, ICorridaService corridaService, IModel model)
    {
        _corridaQueueConsumer = corridaQueueConsumer;
        _corridaService = corridaService;
        _channel = model;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await ProcessarMensagem();
            //var consumer = new EventingBasicConsumer(_channel);

            //consumer.Received += (sender, eventArgs) =>
            //{
            //    var contentByteArray = eventArgs.Body.ToArray();
            //    var contentString = Encoding.UTF8.GetString(contentByteArray);
            //    var message = JsonConvert.DeserializeObject<Corrida>(contentString);

            //    _corridaService.ProcessarSolicitacaoCorrida(message);

            //    _channel.BasicAck(eventArgs.DeliveryTag, false);
            //};

            //_channel.BasicConsume("nova-corrida-queue", false, consumer);
            //return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }

    private async Task ProcessarMensagem()
    {
        await _corridaQueueConsumer.ConsumirMensagem(x =>
        {
            _corridaService.ProcessarSolicitacaoCorrida(x);
        });
    }
}
