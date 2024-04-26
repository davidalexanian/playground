using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace SonarqubeSampleProject.SamplePublisherConsumer
{
    public class SampleEventPublisherCopy : BackgroundService
    {
        readonly IBus _bus;

        public SampleEventPublisherCopy(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new SampleEvent { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                await Task.Delay(2500, stoppingToken);
            }
        }
    }

}
