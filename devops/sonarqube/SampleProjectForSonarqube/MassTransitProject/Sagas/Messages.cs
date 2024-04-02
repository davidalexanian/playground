using MassTransit;
using System;

namespace MassTransitProject.Sagas
{
    public record OrderSubmitted : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCancelled : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCompleted : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }
}
