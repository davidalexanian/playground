namespace MassTransitProject.Exceptions
{
    public record SampleExceptionEvent()
    {
        public string Value { get; init; }

        public override string ToString() => $"{nameof(SampleExceptionEvent)} - {Value}";
    }
}
