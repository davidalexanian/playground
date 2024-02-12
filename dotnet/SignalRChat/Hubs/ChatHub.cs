using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalRChat.Hubs
{
    public class MessageModel
    {
        public string FromUser { get; set; }

        public string ToUser { get; set; }

        public string Message{ get; set; }
    }

    public class ChatHub : Hub
    {
        private static string AllUsersGroup = "AllUsers";
        private static string ReceiveMethodNameOnClient = "ReceiveMessage";
        private static ConcurrentDictionary<string, string> usersByConnections = new ();

        public async Task SendMessageToAll(string user, string message)
        {
            Console.WriteLine($"User: {user}, connection {this.Context.ConnectionId}, message:{message}");
            var others = Clients.AllExcept(Context.ConnectionId);
            others = Clients.Others;
            await others.SendAsync(ReceiveMethodNameOnClient, user, message);

            // foraround to avoid registrating users
            usersByConnections.AddOrUpdate(user, Context.ConnectionId, (k,v) => Context.ConnectionId);
        }

        [HubMethodName("SendMessageToUser")]    // override method name by attribute
        public async Task SendMessageToUser(MessageModel model)
        {
            if (usersByConnections.TryGetValue(model.ToUser, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync(ReceiveMethodNameOnClient, model.FromUser, model.Message);
            }
        }

        public async Task SendMessageToAllUsersGroup(string user, string message)
        {
            await Clients.Group(AllUsersGroup).SendAsync("ReceiveMessage", user, message);
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