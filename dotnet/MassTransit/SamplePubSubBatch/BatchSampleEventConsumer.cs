using MassTransit;
using MassTransitProject.SamplePubSub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitProject.BatchSamplePubSub
{
    class BatchSampleEventConsumer : IConsumer<Batch<BatchSampleEvent>>
    {
        public async Task Consume(ConsumeContext<Batch<BatchSampleEvent>> context)
        {
            Console.WriteLine($"Batch consumer: got {context.Message.Length} messages");
            await Task.CompletedTask;
        }
    }

}
