using MassTransit;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MassTransitProject.Sagas
{
    /// <summary>
    /// Consumer sagas (choreography example)
    /// </summary>
    public class OrderSagaChoregraphy : ISaga,
        InitiatedBy<OrderSubmittedEvent>,                       // or InitiatedByOrOrchestrates<OrderSubmitted>
        Orchestrates<OrderCompletedEvent>,                      // i.e. consumes OrderCompleted
        Observes<OrderCancelledEvent, OrderSagaChoregraphy>     // i.e. consumes OrderCancelledEvent
    {
        public Guid CorrelationId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? AcceptDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            SubmitDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderSubmittedEvent)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            AcceptDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderCompletedEvent)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        // for events not implementing CorrelationId
        public Expression<Func<OrderSagaChoregraphy, OrderCancelledEvent, bool>> CorrelationExpression =>
            (saga, @event) => @event.OrderId == saga.CorrelationId;

        public Task Consume(ConsumeContext<OrderCancelledEvent> context)
        {
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderCancelledEvent)}: {context.CorrelationId}");
            CancelDate = context.Message.Timestamp;
            return Task.CompletedTask;
        }
    }
}
