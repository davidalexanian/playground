using OpenTelemetry.Resources;
using OpenTelemetry;
using System.Diagnostics.Tracing;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TempConsole {
    class Program
    {
        static Meter meter = new Meter("HatStoreMeter", "1.0.0");

        static async Task Main(string[] args)
        {
            Counter<int> hatsSoldCounter = meter.CreateCounter<int>("hats.sold.counter");
            meter.CreateObservableCounter("hats.sold.counter.observable", )
            meter.cre

            Console.WriteLine("Press any key to exit");
            var counter = 0;
            while (!Console.KeyAvailable)
            {
                // Pretend our store has a transaction each second that sells 4 hats
                Thread.Sleep(1000);
                counter += 4;
                hatsSoldCounter.Add(counter);
                Console.WriteLine(counter);
            }
            Console.WriteLine(hatsSoldCounter.Enabled);
        }
    }
}
