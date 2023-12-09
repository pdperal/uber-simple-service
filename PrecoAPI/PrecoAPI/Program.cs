
using Domain.Interfaces;
using Domain.Services;
using Infra.Cache;
using Microsoft.Extensions.Caching;
namespace GeoLocalizacaoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPrecoService, PrecoService>();
            builder.Services.AddScoped<IRedisService, RedisService>();
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "preco-cache";
                options.Configuration = "localhost:6379";
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}