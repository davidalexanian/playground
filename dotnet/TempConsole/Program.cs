using OpenTelemetry.Resources;
using OpenTelemetry;
using System.Diagnostics.Tracing;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using MessagePack;

namespace TempConsole 
{
    public class ContractlessSample
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {


            var data = new ContractlessSample { MyProperty1 = 99, MyProperty2 = 9999 };
            var bin = MessagePackSerializer.Serialize(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            Console.WriteLine(MessagePackSerializer.ConvertToJson(bin));

            // You can also set ContractlessStandardResolver as the default.
            // (Global state; Not recommended when writing library code)
            MessagePackSerializer.DefaultOptions = MessagePack.Resolvers.ContractlessStandardResolver.Options;

            // Now serializable...
            var bin2 = MessagePackSerializer.Serialize(data);
            Console.WriteLine(MessagePackSerializer.ConvertToJson(bin2));
        }






        static Meter meter = new Meter("HatStoreMeter", "1.0.0");
        static async Task MainOld(string[] args)
        {
            Counter<int> hatsSoldCounter = meter.CreateCounter<int>("hats.sold.counter");
            //meter.CreateObservableCounter("hats.sold.counter.observable", )

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
