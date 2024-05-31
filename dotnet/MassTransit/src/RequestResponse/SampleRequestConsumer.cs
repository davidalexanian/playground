using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitProject.RequestResponse
{
    public class SampleRequestConsumer : IConsumer<SampleRequest>
    {
        public SampleRequestConsumer() { }

        public async Task Consume(ConsumeContext<SampleRequest> context)
        {
            Console.WriteLine($"{nameof(SampleRequestConsumer)} got request: {context.Message}");

            await context.RespondAsync<SampleResponse>(new
            {
                context.Message.OrderId,
                Message = $"msg-{context.Message.OrderId}",
                DateTime = DateTime.UtcNow
            });
        }
    }
}