using System;

namespace MassTransitProject.SampleRequestResponse
{
    public record SampleRequest
    {
        public string OrderId { get; init; }

        public override string ToString() => $"{nameof(SampleRequest)}: {OrderId}";
    }

    public record SampleResponse
    {
        public string OrderId { get; init; }
        public string Message { get; init; }
        public DateTime DateTime { get; init; }

        public override string ToString() => $"{nameof(SampleResponse)} {OrderId} {Message} {DateTime}";
    }
}
