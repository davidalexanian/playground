using MassTransit;
using System;

namespace MassTransitTests.Sagas
{
    public record OrderSubmittedCommand : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCancelledCommand : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }

    public record OrderCompletedCommand : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }

        public DateTime Timestamp { get; init; }

        public Guid CorrelationId => OrderId;
    }
}
