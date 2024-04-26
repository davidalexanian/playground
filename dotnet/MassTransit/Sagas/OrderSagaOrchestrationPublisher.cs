using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitProject.Sagas
{
    public class OrderSagaOrchestrationPublisher : BackgroundService
    {
        readonly IBus bus;

        public OrderSagaOrchestrationPublisher(IBus bus) => this.bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var guid = Guid.NewGuid();
            await bus.Publish(new OrderSubmittedCommand { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(1000);
            await bus.Publish(new OrderCompletedCommand { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(1000);
            var result = await bus.Request<OrderInformationRequest, OrderInformationResponse>(new OrderInformationRequest { OrderId = guid });
            Console.WriteLine(result);

            await Task.Delay(1000);
            await bus.Publish(new OrderCancelledCommand { OrderId = guid, Timestamp = DateTime.Now });
        }
    }
}