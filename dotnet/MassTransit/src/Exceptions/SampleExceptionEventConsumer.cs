using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitProject.Exceptions
{
    public class SampleExceptionEventConsumerRetry1 : IConsumer<SampleExceptionEvent>
    {
        public static int counter = 0;

        public SampleExceptionEventConsumerRetry1() { }

        public async Task Consume(ConsumeContext<SampleExceptionEvent> context)
        {
            await Task.Yield();
            Interlocked.Increment(ref counter);
            Console.WriteLine($"Consumer {nameof(SampleExceptionEventConsumerRetry1)} invoked {counter}");
            
            //throw new ApplicationException("timeout");
        }
    }

    public class SampleExceptionEventConsumerRetry2 : IConsumer<SampleExceptionEvent>
    {
        public static int counter = 0;

        public SampleExceptionEventConsumerRetry2() { }

        public async Task Consume(ConsumeContext<SampleExceptionEvent> context)
        {
            await Task.Yield();
            Interlocked.Increment(ref counter);
            Console.WriteLine($"Consumer {nameof(SampleExceptionEventConsumerRetry2)} invoked {counter}");

            //throw new ApplicationException("shouldHandle");
        }
    }

    public class SampleExceptionEventConsumerRedelivery : IConsumer<SampleExceptionEvent>
    {
        public static int counter = 0;

        public SampleExceptionEventConsumerRedelivery() { }

        public async Task Consume(ConsumeContext<SampleExceptionEvent> context)
        {
            await Task.Yield();
            Interlocked.Increment(ref counter);
            Console.WriteLine($"Consumer {nameof(SampleExceptionEventConsumerRedelivery)} invoked {counter}");

            throw new TimeoutException();
        }
    }

    public class LogAllFaultsConsumer : IConsumer<Fault<SampleExceptionEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<SampleExceptionEvent>> context)
        {
            await Task.Yield();
            Console.WriteLine("FAULT DETECTED: " + context.Message.Message.Value);
        }
    }
}
