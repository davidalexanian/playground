using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitProject.PubSub
{
    public class SampleEventConsumer1 : IConsumer<SampleEvent>
    {
        public SampleEventConsumer1() { }

        public Task Consume(ConsumeContext<SampleEvent> context)
        {
            Console.WriteLine($"{nameof(SampleEventConsumer1)}: {context.Message}");
            return Task.CompletedTask;
        }
    }

}
