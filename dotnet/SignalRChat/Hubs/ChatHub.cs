using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private static string AllUsersGroup = "AllUsers";
        private static string ReceiveMessageMethodNameOnClient = "ReceiveMessage";
        private static string ReceiveBigMessageMethodNameOnClient = "ReceiveBigMessage";
        private static ConcurrentDictionary<string, string> usersByConnections = new ();

        public async Task SendToOtherUsers(string user, string message)
        {
            Console.WriteLine($"User: {user}, connection {this.Context.ConnectionId}, message:{message}");
            var others = Clients.Others;   // or Clients.AllExcept(Context.ConnectionId)
            await others.SendAsync(ReceiveMessageMethodNameOnClient, user, message);

            // foraround to avoid registrating users
            usersByConnections.AddOrUpdate(user, Context.ConnectionId, (k,v) => Context.ConnectionId);
        }

        [HubMethodName(nameof(SendMessageToUser))]    // override method name by attribute
        public async Task SendMessageToUser(SendMessageToUserRequest model)
        {
            Console.WriteLine($"FromUser: {model.FromUser}, ToUser: {model.ToUser}, connection {this.Context.ConnectionId}, message:{model.Message}");
            if (usersByConnections.TryGetValue(model.ToUser ?? string.Empty, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync(ReceiveMessageMethodNameOnClient, model.FromUser, model.Message);
            }
        }

        public async Task SendBigPayloadToCaller(string user)
        {
            Console.WriteLine($"User: {user}, {nameof(SendBigPayloadToCaller)}");
            var payload = new BigPayload
            {
                A1 = new string('1', 1000),
                A2 = new string('2', 1000),
                A3 = new string('3', 1000),
                A4 = new string('4', 1000),
                A5 = new string('5', 1000),
            };
            await Clients.Caller.SendAsync(ReceiveBigMessageMethodNameOnClient, payload);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Adding connection {Context.ConnectionId} to group {AllUsersGroup}");
            await Groups.AddToGroupAsync(Context.ConnectionId, AllUsersGroup);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Removing connection {Context.ConnectionId} from group ${AllUsersGroup}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, AllUsersGroup);
            await base.OnDisconnectedAsync(exception);
        }
    }
}