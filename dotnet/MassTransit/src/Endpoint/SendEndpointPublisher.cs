using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitProject.Endpoint
{
    // usage scenario: save multiple messages to storage
    public class SendEndpointPublisher : BackgroundService
    {
        readonly IBus bus;
        private int counter = 0;
        
        public SendEndpointPublisher(IBus bus) 
        {
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // send to exchange or queue directly
                var exchangeAddress = await bus.GetSendEndpoint(new Uri($"queue:{SendEndpointEvent.Url}"));
                var queueAddress = await bus.GetSendEndpoint(new Uri($"queue:{SendEndpointEvent.Url}")); // or bus.GetPublishSendEndpoint for publishing
                await exchangeAddress.Send(new SendEndpointEvent { Value = nameof(SendEndpointPublisher) + $" {++counter}" });
                await queueAddress.Send(new SendEndpointEvent { Value = nameof(SendEndpointPublisher) + $" {++counter}" });

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

}
