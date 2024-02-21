using MessagePack;
using System.Runtime.Serialization;

namespace SignalRChat.Hubs
{
    public enum MyEnum { Aa, Bb, Cc }

    [DataContract]
    public class SendMessageToUserRequest
    {
        [DataMember(Name = "fromUser")]
        public string? FromUser { get; set; }

        [DataMember(Name = "toUser")] 
        public string? ToUser { get; set; }

        [DataMember(Name = "message")] 
        public string? Message { get; set; }

        [DataMember(Name = "dict")] 
        public Dictionary<string, object> Dict { get; set; }

        [DataMember(Name = "myEnumProperty")]
        public MyEnum? MyEnumProperty { get; set; }

    }

    public class ClockPayload
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }
    }

    [MessagePackObject]
    public class BigPayload
    {
        [Key("a1")]
        public string? A1 { get; set; }

        [Key( "a2")] 
        public string? A2 { get; set; }

        [Key( "a3")] 
        public string? A3 { get; set; }

        [Key( "a4")] 
        public string? A4 { get; set; }

        [Key( "a5")] 
        public string? A5 { get; set; }

        [Key("dict")] 
        public Dictionary<string, string> Dict { get; set; }
    }
}
