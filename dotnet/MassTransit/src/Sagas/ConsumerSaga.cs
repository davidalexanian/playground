using MassTransit;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MassTransitProject.Sagas
{
    /// <summary>
    /// Consumer saga (class that combines the state and behavior, its not choregrpahy or smth else, just massTransit thing)
    /// </summary>
    public class ConsumerSaga : ISaga,
        InitiatedBy<OrderSubmittedEvent>,                       // or InitiatedByOrOrchestrates<OrderSubmitted>
        Orchestrates<OrderCompletedEvent>,                      // i.e. consumes OrderCompleted
        Observes<OrderCancelledEvent, ConsumerSaga>             // i.e. consumes OrderCancelledEvent
    {
        public Guid CorrelationId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? AcceptDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            SubmitDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(ConsumerSaga)} consume {nameof(OrderSubmittedEvent)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            AcceptDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(ConsumerSaga)} consume {nameof(OrderCompletedEvent)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        // for events not implementing CorrelationId
        public Expression<Func<ConsumerSaga, OrderCancelledEvent, bool>> CorrelationExpression =>
            (saga, @event) => @event.OrderId == saga.CorrelationId;

        public Task Consume(ConsumeContext<OrderCancelledEvent> context)
        {
            Console.WriteLine($"{nameof(ConsumerSaga)} consume {nameof(OrderCancelledEvent)}: {context.CorrelationId}");
            CancelDate = context.Message.Timestamp;
            return Task.CompletedTask;
        }
    }
}
