using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitTests
{
    public record SampleEvent() { public string Value { get; init; } }

    public class SamplePublisher : BackgroundService
    {
        readonly IBus _bus;

        public SamplePublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new SampleEvent { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                await Task.Delay(3000, stoppingToken);
            }
        }
    }

}
