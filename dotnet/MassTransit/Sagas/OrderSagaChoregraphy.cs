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
        InitiatedBy<OrderSubmitted>,            // or InitiatedByOrOrchestrates<OrderSubmitted>
        Orchestrates<OrderCompleted>,           // i.e. consumes OrderCompleted
        Observes<OrderCancelled, OrderSagaChoregraphy>
    {
        public Guid CorrelationId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? AcceptDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            SubmitDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderSubmitted)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<OrderCompleted> context)
        {
            AcceptDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderCompleted)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        // for events not implementing CorrelationId
        public Expression<Func<OrderSagaChoregraphy, OrderCancelled, bool>> CorrelationExpression =>
            (saga, @event) => @event.OrderId == saga.CorrelationId;

        public Task Consume(ConsumeContext<OrderCancelled> context)
        {
            Console.WriteLine($"{nameof(OrderSagaChoregraphy)} consume {nameof(OrderCancelled)}: {context.CorrelationId}");
            CancelDate = context.Message.Timestamp;
            return Task.CompletedTask;
        }
    }
}
