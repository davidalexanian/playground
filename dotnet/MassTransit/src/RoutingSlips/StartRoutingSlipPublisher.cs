using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitProject.RoutingSlips;
using Microsoft.Extensions.Hosting;

namespace MassTransitProject.RoutingSlips
{
    public class StartRoutingSlipPublisher : BackgroundService
    {
        readonly IBus _bus;

        public StartRoutingSlipPublisher(IBus bus) => _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000);
            var rs = Helper.CreateRoutingSlip();

            await _bus.Execute(rs).ConfigureAwait(false);
        }
    }

}
