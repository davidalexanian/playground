namespace MassTransitProject.SendCommand
{
    public record SendCommand
    {
        public string Value { get; set; }

        public override string ToString() => $"{nameof(SendCommand)} - {Value}";
    }
}
