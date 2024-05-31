using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitProject.SendCommand
{
    // both will get or none will get the command
    public class SampleCommandHandler1 : IConsumer<SendCommand>
    {
        public SampleCommandHandler1() { }

        public Task Consume(ConsumeContext<SendCommand> context)
        {
            Console.WriteLine($"{nameof(SampleCommandHandler1)}: {context.Message}");
            return Task.CompletedTask;
        }
    }

    public class SampleCommandHandler2 : IConsumer<SendCommand>
    {
        public SampleCommandHandler2() { }

        public Task Consume(ConsumeContext<SendCommand> context)
        {
            Console.WriteLine($"{nameof(SampleCommandHandler2)}: {context.Message}");
            return Task.CompletedTask;
        }
    }

}
