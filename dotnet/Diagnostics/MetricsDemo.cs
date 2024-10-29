using System.Diagnostics.Metrics;

namespace Diagnostics
{
    internal class MetricsDemo
    {
        static Meter meter = new Meter("HatCo.Store");

        static Counter<int> hatsSold = meter.CreateCounter<int>("hatco.store.hats_sold", "unit", "description", new[]
        {
            new KeyValuePair<string, object?>("product.version", "1.2.1"),
            new KeyValuePair<string, object?>("product.name", "tax")
        });
        static Histogram<double> orderProcessingTime = meter.CreateHistogram<double>("hatco.store.order_processing_time");
        static int coatsSold;

        public static void Demo()
        {
            Console.WriteLine("Metrics Demo");
            Console.WriteLine();
            Console.WriteLine();

            meter.CreateObservableCounter<int>("hatco.store.coats_sold", () => coatsSold);
            meter.CreateObservableGauge<int>("hatco.store.orders_pending", () => GetOrdersPending());

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1000);

                // Pretend our store has one transaction each 100ms that each sell 4 hats and 3 coats
                hatsSold.Add(1);
                hatsSold.Add(2);
                coatsSold += 3;

                // Last we pretend that we measured how long it took to do the transaction (for example we could time it with Stopwatch)
                orderProcessingTime.Record(new Random().Next(5, 15) / 1000.0);
            }

            Console.WriteLine();
            Console.WriteLine();

            static IEnumerable<Measurement<int>> GetOrdersPending() => 
                [
                    new Measurement<int>(6, new KeyValuePair<string,object?>("customer.country", "Italy")),
                    new Measurement<int>(3, new KeyValuePair<string,object?>("customer.country", "Spain")),
                    new Measurement<int>(1, new KeyValuePair<string,object?>("customer.country", "Mexico")),
                ];
        }
    }
}
