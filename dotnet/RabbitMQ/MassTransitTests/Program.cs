using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransitTests;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();
                        x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();
                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);
                        x.AddRequestClient<SampleRequest>();

                        //x.UsingInMemory((context, cfg) => {
                        //    cfg.ConfigureEndpoints(context);
                        //});

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h => { h.Username("admin"); h.Password("admin"); });
                            cfg.ConfigureEndpoints(context);
                        });

                        x.AddHostedService<RequestResponseSender>();
                        //x.AddHostedService<SamplePublisher>();
                    });
                });
    }
}