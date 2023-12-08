using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
