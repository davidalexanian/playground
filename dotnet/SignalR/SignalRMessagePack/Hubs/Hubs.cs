using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace SignalRMessagePack.Hubs
{
    public class MessagePackHub : BaseHub { }

    public class JsonHub : BaseHub { }

    public abstract class BaseHub : Hub
    {
        public async Task SendToServerButtonClick(PayloadModel payload)
        {
            await Task.Yield();
            Console.WriteLine($"Method:{nameof(SendToServerButtonClick)}, connection:{this.Context.ConnectionId}, payload:{JsonSerializer.Serialize(payload)}");
        }

        public async Task SendFromServerButtonClick()
        {
            var payload = PayloadModel.Create();
            Console.WriteLine($"Method:{nameof(SendToServerButtonClick)}, connection:{this.Context.ConnectionId}, payload:{JsonSerializer.Serialize(payload)}");
            await Clients.All.SendAsync("ReceiveMessageFromServer", payload);
        }
    }
}