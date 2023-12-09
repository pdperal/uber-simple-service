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

namespace Infra
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IMotoristaRepository, MotoristaRepository>();

            return collection;
        }
    
        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddScoped<IMotoristaService, MotoristaService>();
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
