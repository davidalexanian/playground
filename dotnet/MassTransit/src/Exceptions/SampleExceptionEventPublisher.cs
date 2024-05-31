using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.Exceptions
{
    public class SampleExceptionEventPublisher : BackgroundService
    {
        readonly IBus _bus;

        public SampleExceptionEventPublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000, stoppingToken);
            await _bus.Publish(new SampleExceptionEvent { Value = $"send" }, stoppingToken);
        }
    }

}
