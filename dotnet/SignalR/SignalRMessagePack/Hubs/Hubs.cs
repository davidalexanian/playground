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

        public async Task SendToServerArgumentsButtonClick(
            Guid PropId1,
            int? PropInt,
            string? PropString,
            decimal PropDecimal,
            string PropDecimalString,
            decimal? PropDecimalNullable,
            double? PropDouble,
            DateTime PropDateTime,
            DateTime? PropDateTimeNullable,
            DateTimeOffset PropDateTimeOffset,
            DateTimeOffset? PropDateTimeOffsetNullable,
            MyEnum? PropEnum,
            List<ModelItem>? PropItems,
            Dictionary<string, object>? PropDict)
        {
            await Task.Yield();
            Console.WriteLine($"Method:{nameof(SendToServerArgumentsButtonClick)}");
        }

        public async Task SendFromServerButtonClick()
        {
            var payload = PayloadModel.Create();
            Console.WriteLine($"Method:{nameof(SendToServerButtonClick)}, connection:{this.Context.ConnectionId}, payload:{JsonSerializer.Serialize(payload)}");
            await Clients.All.SendAsync("ReceiveMessageFromServer", payload);
        }
    }
}