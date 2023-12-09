using WorkerService.Extension;
using WorkerService.Jobs;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddRepositories();
                    services.AddServices();
                    services.AddMensageria();
                    services.AddConnectionString();
                    services.AddHostedService<CorridaConsumerJob>();

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.InstanceName = "motorista-cache";
                        options.Configuration = "localhost:6379";
                    });
                })
                .Build();

            host.Run();
        }
    }
}