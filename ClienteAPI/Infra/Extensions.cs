using Domain.Interfaces.Data;
using Domain.Interfaces.IntegrationService;
using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.IntegrationService;
using Infra.Mensageria;
using Infra.Mensageria.Exchange;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infra
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IClienteRepository, ClienteRepository>();

            return collection;
        }

        public static IServiceCollection AddMensageria(this IServiceCollection collection)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            var connection = connectionFactory.CreateConnection("cliente-api");

            var connectionProducer = new ProducerConnection(connection);
            var channel = connectionProducer.Connection.CreateModel();
            channel.ExchangeDeclare("nova-corrida-exchange", "direct", true);

            collection.AddSingleton(channel);

            collection.AddSingleton<ICorridaExchange, CorridaExchange>();

            return collection;
        }

        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddScoped<IClienteService, ClienteService>();
            collection.AddScoped<ICotacaoService, CotacaoService>();
            collection.AddScoped<ICorridaService, CorridaService>();
            collection.AddScoped<IPrecoAPI, PrecoAPI>();

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
