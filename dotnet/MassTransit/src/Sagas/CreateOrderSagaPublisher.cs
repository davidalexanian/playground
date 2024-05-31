using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitProject.Sagas
{
    public class CreateOrderSagaPublisher : BackgroundService
    {
        readonly IBus bus;

        public CreateOrderSagaPublisher(IBus bus) => this.bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var guid = Guid.NewGuid();

            await bus.Publish(new OrderSubmittedEvent { OrderId = guid, Timestamp = DateTime.Now });
            await Task.Delay(3000);

            //await bus.Publish(new OrderCompletedEvent { OrderId = guid, Timestamp = DateTime.Now });
            await Task.Delay(3000);

            var result = await bus.Request<OrderInformationRequest, OrderInformationResponse>(new OrderInformationRequest { OrderId = guid });
            Console.WriteLine(result);
        }
    }
}