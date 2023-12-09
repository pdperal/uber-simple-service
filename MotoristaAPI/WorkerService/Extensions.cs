using Application.Jobs;
using Domain.Interfaces.Data;
using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.Mensageria;
using Infra.Mensageria.Queue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace WorkerService.Extension
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddSingleton<IMotoristaRepository, MotoristaRepository>();

            return collection;
        }
        public static IServiceCollection AddMensageria(this IServiceCollection collection)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            var connection = connectionFactory.CreateConnection("motorista-consumer");

            var connectionProducer = new ConsumerConnection(connection);
            var channel = connectionProducer.Connection.CreateModel();
            channel.ExchangeDeclare("nova-corrida-exchange", "direct", true);
            channel.QueueDeclare("nova-corrida-queue", false, false, false, null);
            channel.QueueBind("nova-corrida-queue", "nova-corrida-exchange", "nova-corrida-queue");

            collection.AddSingleton(channel);

            collection.AddSingleton<ICorridaQueueConsumer, CorridaQueueConsumer>();

            return collection;
        }

        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddSingleton<IMotoristaService, MotoristaService>();
            collection.AddSingleton<ICorridaService, CorridaService>();

            return collection;
        }

        public static IServiceCollection AddConnectionString(this IServiceCollection collection)
        {
            collection.AddSingleton(x =>
            {
                var config = x.GetService<IConfiguration>();
                var connection = new ConnectionString();
                config.GetSection("ConnectionStrings").Bind(connection);

                return connection;
            });

            return collection;
        }
    }
}
