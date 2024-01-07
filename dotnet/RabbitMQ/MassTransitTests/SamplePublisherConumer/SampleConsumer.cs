using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MassTransitProject.SamplePublisherConumer
{
    public class SampleConsumer : IConsumer<SampleEvent>
    {
        public SampleConsumer() { }

        public Task Consume(ConsumeContext<SampleEvent> context)
        {
            Console.WriteLine($"{nameof(SampleConsumer)} - received text: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }

}
