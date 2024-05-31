using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitProject.Endpoint
{
    public class SendEndpointConsumer : IConsumer<SendEndpointEvent>
    {
        public SendEndpointConsumer() { }

        public Task Consume(ConsumeContext<SendEndpointEvent> context)
        {
            Console.WriteLine($"{nameof(SendEndpointConsumer)} got a message: {context.Message}");
            return Task.CompletedTask;
        }
    }
}
