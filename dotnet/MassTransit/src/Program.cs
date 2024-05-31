using MassTransit;
using MassTransitProject.RoutingSlips;
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

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();
                        x.SetInMemorySagaRepositoryProvider();

                        // RequestResponse
                        //x.AddRequestClient<SampleRequest>();
                        //x.AddHostedService<SampleRequestSender>();
                        //x.AddConsumer<SampleRequestConsumer>();

                        // PubSub
                        //x.AddHostedService<SampleEventPublisher>();
                        //x.AddConsumer<SampleEventConsumer1>();
                        //x.AddConsumer<SampleEventConsumer2>();

                        // PubSubBatch
                        //x.AddHostedService<BatchSampleEventPublisher>();
                        //x.AddConsumer<BatchSampleEventConsumer>().Endpoint(cnf => { cnf.ConcurrentMessageLimit = 10; cnf.PrefetchCount = 200; });

                        // SendCommand
                        // x.AddHostedService<SampleCommandPublisher>();
                        // x.AddConsumer<SampleCommandHandler1>();
                        // x.AddConsumer<SampleCommandHandler2>();

                        // SendToEndpoint
                        //x.AddHostedService<SendEndpointPublisher>();
                        //x.AddConsumer<SendEndpointConsumer>();

                        // Exceptions
                        //x.AddHostedService<SampleExceptionEventPublisher>();
                        //x.AddConsumer<LogAllFaultsConsumer>();
                        //x.AddConsumer<SampleExceptionEventConsumerRetry1>(cnf =>
                        //{
                        //     retry locally (not good for consumers taking long time)
                        //    cnf.UseMessageRetry(c => 
                        //    {
                        //        c.Interval(3, 2000);    // retry 3 times, pause 2 seconds
                        //         do not retry when this happens (for all other exceptions, it will retry)
                        //        c.Ignore<TimeoutException>();
                        //    });
                        //});
                        //x.AddConsumer<SampleExceptionEventConsumerRetry2>(cnf =>
                        //{
                        //    cnf.UseMessageRetry(c =>
                        //    {
                        //        c.Interval(3, 2000);    // retry 3 times, pause 2econds
                        //         retry for ApplicationException (for all other exceptions it wont be retried)
                        //        c.Handle<ApplicationException>(ex => ex.Message == "shouldHandle");
                        //    });
                        //});
                        // message redelivery is also referred to as second-level retry.
                        // retry 3 times, if still fails (e.g. DB down), redeliver 3 times (requires delayed-exchange plug-in)
                        //x.AddConsumer<SampleExceptionEventConsumerRedelivery>(cnf =>
                        //{
                        //    cnf.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(7)));
                        //    cnf.UseMessageRetry(r =>
                        //    {
                        //        r.Immediate(3);
                        //        r.Ignore<TimeoutException>();
                        //    });
                        //});

                        // routing slip
                        x.AddActivitiesFromNamespaceContaining<RegisterForRaceActivity>();
                        x.AddHostedService<StartRoutingSlipPublisher>();

                        // configuration
                        // x.AddConsumers(entryAssembly);
                        // x.AddActivities(entryAssembly);
                        // x.AddSagas(entryAssembly);
                        // x.AddSagaStateMachines(entryAssembly);

                        // sagas
                        // x.AddSaga<ConsumerSaga>().InMemoryRepository();
                        // x.AddHostedService<ConsumerSagaPublisher>();
                        // x.AddSagaStateMachine<CreateOrderSaga, CreateOrderState>().InMemoryRepository();
                        // x.AddHostedService<CreateOrderSagaPublisher>();
                        // x.AddConsumer<CreateOrderVerifyCustomerCommandHandler>();

                        // transport configuration
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
}