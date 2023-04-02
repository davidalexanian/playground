using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitTests.RequestResponse
{
    public record SampleRequest
    {
        public string OrderId { get; init; }
    }

    public record SampleResponse
    {
        public string OrderId { get; init; }
        public string Message { get; init; }
        public DateTime DateTime { get; init; }
    }
    public class RequestResponseSender : BackgroundService
    {

        readonly IBus _bus;

        public RequestResponseSender(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // create manually or just inject IRequestClient<SampleRequest> _client;
            var client = _bus.CreateRequestClient<SampleRequest>();

            int i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await client.GetResponse<SampleResponse>(new { OrderId = i++ });
                Console.WriteLine($"{nameof(RequestResponseSender)} got response: {response.Message.OrderId}-{response.Message.DateTime}");
                await Task.Delay(2000, stoppingToken);
            }
        }
    }

}
