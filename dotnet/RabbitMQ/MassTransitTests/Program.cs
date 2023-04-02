using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MassTransitTests.Sagas;
using MassTransitTests.RequestResponse;
using MassTransitTests.SamplePublisherConumer;

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
                        x.AddActivities(entryAssembly);
                        x.AddRequestClient<SampleRequest>();

                        // sagas
                        x.AddSaga<OrderSaga>().InMemoryRepository();
                        x.AddSagaStateMachine<OrderStateMachine, OrderState>().InMemoryRepository();

                        // or use 
                        //x.AddSagas(entryAssembly);
                        //x.AddSagaStateMachines(entryAssembly);

                        // message senders (uncomment to execute)
                        // x.AddHostedService<RequestResponseSender>();
                        // x.AddHostedService<SamplePublisher>();
                        // x.AddHostedService<OrderSagaPublisher>();
                        x.AddHostedService<OrderStateMachinePublisher>();

                        // transport
                        //x.UsingInMemory((context, cfg) => {
                        //    cfg.ConfigureEndpoints(context);
                        //});
                        x.UsingRabbitMq((context, cfg) => {
                            cfg.Host("localhost", "/", h => { h.Username("admin"); h.Password("admin"); });
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}