using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitProject.SamplePubSub
{
    public class SampleEventConsumer2 : IConsumer<SampleEvent>
    {
        public SampleEventConsumer2() { }

        public Task Consume(ConsumeContext<SampleEvent> context)
        {
            Console.WriteLine($"{nameof(SampleEventConsumer2)}: {context.Message}");
            return Task.CompletedTask;
        }
    }

}
