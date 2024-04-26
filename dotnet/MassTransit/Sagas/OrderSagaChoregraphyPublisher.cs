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
            await bus.Publish(new OrderSubmittedEvent { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(2000);
            await bus.Publish(new OrderCompletedEvent { OrderId = guid, Timestamp = DateTime.Now });

            await Task.Delay(2000);
            await bus.Publish(new OrderCancelledEvent { OrderId = guid, Timestamp = DateTime.Now });
        }
    }
}
