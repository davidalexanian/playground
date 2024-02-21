using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<ClockHub, IClock> _clockHub;

        public Worker(ILogger<Worker> logger, IHubContext<ClockHub, IClock> clockHub)
        {
            _logger = logger;
            _clockHub = clockHub;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000, stoppingToken);
                await _clockHub.Clients.All.ShowTime(new ClockPayload 
                {
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToLongTimeString(),
                    TimeZone = TimeZoneInfo.Local.StandardName
                });
            }
        }
    }
}
