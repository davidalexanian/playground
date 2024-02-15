using MessagePack;

namespace SignalRChat.Hubs
{
    [MessagePackObject]
    public class SendMessageToUserRequest
    {
        [Key("fromUser")]
        public string? FromUser { get; set; }

        [Key("toUser")]
        public string? ToUser { get; set; }

        [Key("message")]
        public string? Message { get; set; }
    }

    public class BigPayload
    {
        public string? A1 { get; set; }
        public string? A2 { get; set; }
        public string? A3 { get; set; }
        public string? A4 { get; set; }
        public string? A5 { get; set; }
    }
}
