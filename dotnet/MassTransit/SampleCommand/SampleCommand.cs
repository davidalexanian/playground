namespace MassTransitProject.SampleCommand
{
    public record SampleCommand
    {
        public string Value { get; set; }

        public override string ToString() => $"{nameof(SampleCommand)} - {Value}";
    }
}
