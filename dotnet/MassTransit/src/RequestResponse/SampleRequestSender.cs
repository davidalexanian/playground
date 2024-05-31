using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.RequestResponse
{
    public class SampleRequestSender : BackgroundService
    {

        readonly IBus _bus;

        public SampleRequestSender(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // create manually or just inject IRequestClient<SampleRequest> _client;
            var client = _bus.CreateRequestClient<SampleRequest>();

            int i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await client.GetResponse<SampleResponse>(new { OrderId = i++ });
                Console.WriteLine($"{nameof(SampleRequestSender)} got response: {response.Message}");
                await Task.Delay(2500, stoppingToken);
            }
        }
    }

}
