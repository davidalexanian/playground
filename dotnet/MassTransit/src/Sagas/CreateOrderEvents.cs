using MassTransit;
using System;

namespace MassTransitProject.Sagas
{
    public record BaseMessage : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; init; }
        public DateTime Timestamp { get; init; }
        public Guid CorrelationId => OrderId;
    }

    // events
    public record OrderSubmittedEvent : BaseMessage { }
    public record OrderCancelledEvent : BaseMessage { }
    public record OrderCompletedEvent : BaseMessage { }

    // commands
    public record CreateOrderVerifyCustomerCommand : BaseMessage
    {
        public string CustomerName { get; set; }
    }
    public record CreateOrderCustomerVerifiedEvent : BaseMessage
    {
        public bool IsValidCustomer { get; set; }
    }

    // request-response models
    public record OrderInformationRequest
    {
        public Guid OrderId { get; set; }
    }
    public record OrderInformationResponse
    {
        public Guid OrderId { get; init; }

        public string State { get; init; }

        public override string ToString() => $"{OrderId}-{State}";
    }
    public record OrderNotFound
    {
        public Guid OrderId { get; set; }
    }
}
