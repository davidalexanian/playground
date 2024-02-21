using MessagePack;
using MessagePack.Formatters;
using System.Globalization;

namespace SignalRMessagePack.Hubs
{
    public class MyDateTimeFormatter : IMessagePackFormatter<DateTime>
    {
        private const string WriteFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        public static readonly MyDateTimeFormatter Instance = new ();

        private MyDateTimeFormatter() { }

        public void Serialize(ref MessagePackWriter writer, DateTime value, MessagePackSerializerOptions options) =>
            writer.Write(value.ToString(WriteFormatString, CultureInfo.InvariantCulture));

        public DateTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) =>
            DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
    }

    public class MyNullableDateTimeFormatter : IMessagePackFormatter<DateTime?>
    {
        private const string WriteFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        public static readonly MyNullableDateTimeFormatter Instance = new();

        private MyNullableDateTimeFormatter() { }

        public void Serialize(ref MessagePackWriter writer, DateTime? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Value.ToString(WriteFormatString, CultureInfo.InvariantCulture));
        }

        public DateTime? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil()) 
            {
                return null;   
            }
            return DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
        }
    }

    public class MyDateTimeOffsetFormatter : IMessagePackFormatter<DateTimeOffset>
    {
        private const string DateFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        public static readonly MyDateTimeOffsetFormatter Instance = new();

        private MyDateTimeOffsetFormatter() { }

        public void Serialize(ref MessagePackWriter writer, DateTimeOffset value, MessagePackSerializerOptions options) =>
            writer.Write(value.ToString(DateFormatString, CultureInfo.InvariantCulture));

        public DateTimeOffset Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) =>
            DateTimeOffset.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
    }

    public class MyNullableDateTimeOffsetFormatter : IMessagePackFormatter<DateTimeOffset?>
    {
        private const string WriteFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        public static readonly MyNullableDateTimeOffsetFormatter Instance = new();

        private MyNullableDateTimeOffsetFormatter() { }

        public void Serialize(ref MessagePackWriter writer, DateTimeOffset? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Value.ToString(WriteFormatString, CultureInfo.InvariantCulture));
        }

        public DateTimeOffset? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil()) 
            { 
                return null;
            }
            var str = reader.ReadString();
            return DateTimeOffset.Parse(str, CultureInfo.InvariantCulture);
        }
    }

    public sealed class MyNullableDecimalFormatter : IMessagePackFormatter<decimal?>
    {
        public static readonly MyNullableDecimalFormatter Instance = new();

        private MyNullableDecimalFormatter() { }

        public void Serialize(ref MessagePackWriter writer, decimal? value, MessagePackSerializerOptions options)
        {
            if (value == null) 
            {
                writer.WriteNil();
                return;
            }
            writer.Write((double)value);
        }

        public decimal? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) =>
            reader.TryReadNil() ? default : (decimal)reader.ReadDouble();
    }

    public sealed class MyDecimalFormatter : IMessagePackFormatter<decimal>
    {
        public static readonly MyDecimalFormatter Instance = new();

        private MyDecimalFormatter() { }

        public void Serialize(ref MessagePackWriter writer, decimal value, MessagePackSerializerOptions options) =>
            writer.Write((double)value);

        public decimal Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) =>
            (decimal)reader.ReadDouble();
    }
}
