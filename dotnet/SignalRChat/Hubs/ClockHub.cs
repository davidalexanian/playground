using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public interface IClock
    {
        Task ShowTime(string currentTime);
    }

    public class ClockHub : Hub<IClock>
    {
        public async Task SendTimeToClients(string currentTime)
        {
            await Clients.All.ShowTime(currentTime);
        }
    }
}
