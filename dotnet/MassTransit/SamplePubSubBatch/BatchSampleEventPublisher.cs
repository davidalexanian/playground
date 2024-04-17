using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitProject.SamplePubSub;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.BatchSamplePubSub
{
    // usage scenario: save multiple messages to storage
    public class BatchSampleEventPublisher : BackgroundService
    {
        readonly IBus _bus;

        public BatchSampleEventPublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var list = new List<BatchSampleEvent>();
                for (int i = 1; i <= 10; i++)
                {
                    var message = new BatchSampleEvent { Value = $"Message number {i}" };
                    await _bus.Publish(message, stoppingToken);
                    list.Add(message);
                }

                await _bus.PublishBatch(list, stoppingToken);
                list.Clear();

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

}
