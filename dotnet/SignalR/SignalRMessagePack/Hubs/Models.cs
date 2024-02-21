using MessagePack;

namespace SignalRMessagePack.Hubs
{
    [MessagePackObject(true)]
    public class PayloadModel
    {
        public Guid? PropId { get; set; }

        public int? PropInt { get; set; }

        public string? PropString { get; set; }

        public decimal PropDecimal { get; set; }

        public decimal? PropDecimalNullable { get; set; }

        public double? PropDouble { get; set; }

        public DateTime PropDateTime { get; set; }

        public DateTime? PropDateTimeNullable { get; set; }

        public DateTimeOffset PropDateTimeOffset { get; set; }

        public DateTimeOffset? PropDateTimeOffsetNullable { get; set; }

        public MyEnum? PropEnum { get; set; }

        public List<ModelItem>? PropItems { get; set; }

        public Dictionary<string, object>? PropDict { get; set; }

        public static PayloadModel Create() => new PayloadModel()
        {
            PropId = Guid.NewGuid(),
            PropInt = 10,
            PropString = "str",
            PropDecimal = 123456789.123456789M,
            PropDecimalNullable = null,
            PropDouble = 123456789.123456789D,
            PropDateTime = DateTime.Now,
            PropDateTimeNullable = null,
            PropDateTimeOffset = DateTimeOffset.Now,
            PropDateTimeOffsetNullable = null,
            PropEnum = MyEnum.Cc,
            PropItems = new List<ModelItem> 
            {
                new ModelItem { ItemId = 1, ItemName = "Name1" },
                new ModelItem { ItemId = 2, ItemName = "Name2" }
            },
            PropDict = new Dictionary<string, object>()
            {
                { "aa", "Aaa" },
                { "bb", 11 },
                { "cc", new { Cc1 = 1, Cc2 = "Cc2" } },
                { "dd", new ModelItem { ItemId = 1, ItemName = "Name1" } },
            }
        };
    }

    [MessagePackObject(true)]
    public class ModelItem
    {
        public int? ItemId { get; set; }

        public string? ItemName { get; set; }
    }

    public enum MyEnum { Aa, Bb, Cc }
}
