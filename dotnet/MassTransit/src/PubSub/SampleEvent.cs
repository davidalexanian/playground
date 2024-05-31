namespace MassTransitProject.PubSub
{
    public record SampleEvent()
    {
        public string Value { get; init; }

        public override string ToString() => $"{nameof(SampleEvent)} - {Value}";
    }
}
