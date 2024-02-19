using MessagePack;
using System.Runtime.Serialization;

namespace SignalRChat.Hubs
{
    public class SendMessageToUserRequest
    {
        public string? FromUser { get; set; }

        public string? ToUser { get; set; }

        public string? Message { get; set; }
    }

    public class ClockPayload
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }
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
