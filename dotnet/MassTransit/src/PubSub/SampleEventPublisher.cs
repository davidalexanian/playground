using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.PubSub
{
    public class SampleEventPublisher : BackgroundService
    {
        readonly IBus _bus;

        public SampleEventPublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new SampleEvent { Value = $"Published at {DateTimeOffset.Now}" }, stoppingToken);

                var endpoint = await _bus.GetPublishSendEndpoint<SampleEvent>();
                await endpoint.Send(new SampleEvent { Value = "send" });
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

}
