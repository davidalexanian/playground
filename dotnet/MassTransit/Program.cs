using MassTransit;
using MassTransitProject.Endpoints;
using MassTransitProject.SampleCommand;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MassTransitProject
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

                        // SampleRequestResponse
                        //x.AddRequestClient<SampleRequest>();
                        //x.AddHostedService<SampleRequestSender>();
                        //x.AddConsumer<SampleRequestConsumer>();

                        // SamplePubSub
                        //x.AddHostedService<SampleEventPublisher>();
                        //x.AddConsumer<SampleEventConsumer1>();
                        //x.AddConsumer<SampleEventConsumer2>();

                        // SamplePubSubBatch
                        //x.AddHostedService<BatchSampleEventPublisher>();
                        //x.AddConsumer<BatchSampleEventConsumer>().Endpoint(cnf => { cnf.ConcurrentMessageLimit = 10; cnf.PrefetchCount = 200; });

                        // SampleCommand
                        // x.AddHostedService<SampleCommandPublisher>();
                        // x.AddConsumer<SampleCommandHandler1>();
                        // x.AddConsumer<SampleCommandHandler2>();

                        // use endpoint address
                        x.AddHostedService<SendEndpointPublisher>();
                        x.AddConsumer<SendEndpointConsumer>();

                        // var entryAssembly = Assembly.GetEntryAssembly();
                        // x.AddConsumers(entryAssembly);
                        // x.AddActivities(entryAssembly);
                        //x.AddSagas(entryAssembly);
                        //x.AddSagaStateMachines(entryAssembly);

                        //x.AddSaga<OrderSaga>().InMemoryRepository();
                        //x.AddSagaStateMachine<OrderStateMachine, OrderState>().InMemoryRepository();

                        // message senders (uncomment to execute)
                        // x.AddHostedService<SamplePublisher>();
                        // x.AddHostedService<OrderSagaPublisher>();
                        // x.AddHostedService<OrderStateMachinePublisher>();

                        // transport
                        //x.UsingInMemory((context, cfg) =>
                        //{
                        //    cfg.ConfigureEndpoints(context);
                        //});
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}