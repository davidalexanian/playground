namespace MassTransitProject.SamplePubSub
{
    public record BatchSampleEvent()
    {
        public int Number { get; set; }
        public string Value { get; init; }

        public override string ToString() => $"{nameof(SampleEvent)} - {Value}";
    }
}
