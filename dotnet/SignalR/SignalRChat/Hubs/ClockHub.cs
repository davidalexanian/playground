using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public interface IClock
    {
        Task ShowTime(ClockPayload clockPayload);
    }

    public class ClockHub : Hub<IClock>
    {
        public async Task SendTimeToClients(ClockPayload clockPayload)
        {
            await Clients.All.ShowTime(clockPayload);
        }
    }
}
