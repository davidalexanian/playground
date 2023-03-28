using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MassTransitTests
{
    public class SampleConsumer : IConsumer<SampleEvent>
    {
        readonly ILogger<SampleConsumer> _logger;

        public SampleConsumer(ILogger<SampleConsumer> logger) {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SampleEvent> context)
        {
            _logger.LogInformation($"{nameof(SampleConsumer)} - received text: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }

}
