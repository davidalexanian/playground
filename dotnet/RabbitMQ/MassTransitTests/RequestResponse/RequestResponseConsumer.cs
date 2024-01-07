using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitProject.RequestResponse
{
    public class RequestResponseConsumer : IConsumer<SampleRequest>
    {

        public RequestResponseConsumer() { }

        public async Task Consume(ConsumeContext<SampleRequest> context)
        {
            await context.RespondAsync<SampleResponse>(new
            {
                context.Message.OrderId,
                Message = $"msg-{context.Message.OrderId}",
                DateTime = DateTime.UtcNow
            });
        }
    }
}