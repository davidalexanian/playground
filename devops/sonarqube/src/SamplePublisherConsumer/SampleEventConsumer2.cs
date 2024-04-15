using System;
using System.Threading.Tasks;
using MassTransit;

namespace SonarqubeSampleProject.SamplePublisherConsumer
{
    public class SampleEventConsumer2 : IConsumer<SampleEvent>
    {
        public SampleEventConsumer2() { }

        public Task Consume(ConsumeContext<SampleEvent> context)
        {
            Console.WriteLine($"{nameof(SampleEventConsumer1)}: {context.Message}");
            return Task.CompletedTask;
        }
    }

}
