using AutoFixture;
using MessagePack;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace TempConsole
{

    public class Model
    {
        public string MyPropertyString { get; set; }

        public DateTime MyPropertyDateTime { get; set; }

        public void OnBeforeSerialize()
        {
            Console.WriteLine("OnBefore");
        }

        public void OnAfterDeserialize()
        {
            Console.WriteLine("OnAfter");
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var fixture = new AutoFixture.Fixture();
            var data = fixture.Create<Model>();

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize<Model>(data, 
                new System.Text.Json.JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            var binData = MessagePackSerializer.Serialize(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);

            var data2 = MessagePackSerializer.Deserialize<Model>(binData, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize<Model>(data,
                new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
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
