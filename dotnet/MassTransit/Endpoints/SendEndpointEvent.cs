using MassTransit;

namespace MassTransitProject.Endpoints
{
    [EntityName(Url)]
    public record SendEndpointEvent
    {
        public const string Url = "my-custom-endpoint";

        public string Value { get; set; }

        public override string ToString() => $"{nameof(SendEndpointEvent)} - {Value}";
    }
}
