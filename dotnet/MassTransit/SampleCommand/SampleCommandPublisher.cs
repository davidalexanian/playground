using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.SampleCommand
{
    // execute in two different applications
    public class SampleCommandPublisher : BackgroundService
    {
        readonly IBus _bus;

        public SampleCommandPublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var endpoint = await _bus.GetPublishSendEndpoint<SampleCommand>();
                await endpoint.Send(new SampleCommand { Value = "send" });
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

}
