using MessagePack;

namespace SignalRMessagePack.Hubs
{
    [MessagePackObject()]
    public abstract class PayloadModelBase
    {
        [Key("PropIntBase")]
        public int? PropIntBase { get; set; } = 11;

        [Key("PropStringBase")]
        public string? PropStringBase { get; set; } = "Abc";
    }

    [MessagePackObject()]
    public class PayloadModel
    {
        [Key("PropId")]
        public Guid? PropId { get; set; }

        [Key("PropInt")]
        public int? PropInt { get; set; }

        [Key("PropString")]
        public string? PropString { get; set; }

        [Key("PropDecimal")]
        public decimal PropDecimal { get; set; }

        [Key("PropDecimalString")]
        public string PropDecimalString { get; set; }

        [Key("PropDecimalNullable")]
        public decimal? PropDecimalNullable { get; set; }

        [Key("PropDouble")]
        public double? PropDouble { get; set; }

        [Key("PropDateTime")]
        public DateTime PropDateTime { get; set; }

        [Key("PropDateTimeNullable")]
        public DateTime? PropDateTimeNullable { get; set; }

        [Key("PropDateTimeOffset")]
        public DateTimeOffset PropDateTimeOffset { get; set; }

        [Key("PropDateTimeOffsetNullable")]
        public DateTimeOffset? PropDateTimeOffsetNullable { get; set; }

        [Key("PropEnum")]
        public MyEnum? PropEnum { get; set; }

        [Key("PropItems")]
        public List<ModelItem>? PropItems { get; set; }

        [Key("PropDict")]
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
            PropDateTimeNullable = DateTime.UtcNow,
            PropDateTimeOffset = DateTimeOffset.Now,
            PropDateTimeOffsetNullable = DateTimeOffset.UtcNow,
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
                { "ee", null },
                { "ff", new object[] {11, "aa", null }}
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
