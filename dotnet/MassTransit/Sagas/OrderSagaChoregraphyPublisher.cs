using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitProject.Sagas
{
    public class OrderSagaChoregraphyPublisher : BackgroundService
    {
        readonly IBus bus;

        public OrderSagaChoregraphyPublisher(IBus bus) => this.bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var guid = Guid.NewGuid();
            await bus.Publish(new OrderSubmitted { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(2000);
            await bus.Publish(new OrderCompleted { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(2000);
            await bus.Publish(new OrderCancelled { OrderId = guid, Timestamp = DateTime.Now });
        }
    }
}
