using MassTransit;
using System;

namespace MassTransitProject.Sagas
{
    public record OrderSubmittedEvent : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCancelledEvent : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCompletedEvent : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }
}
