using MassTransit;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SonarqubeSampleProject.Sagas
{
    /// <summary>
    /// Consumer sagas (choreography example)
    /// </summary>
    public class OrderSaga : ISaga,
        InitiatedBy<OrderSubmitted>,    //or InitiatedByOrOrchestrates<OrderSubmitted>
        Orchestrates<OrderCompleted>,
        Observes<OrderCancelled, OrderSaga>
    {
        public Guid CorrelationId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? AcceptDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            SubmitDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSaga)} consume {nameof(OrderSubmitted)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<OrderCompleted> context)
        {
            AcceptDate = context.Message.Timestamp;
            Console.WriteLine($"{nameof(OrderSaga)} consume {nameof(OrderCompleted)}: {context.CorrelationId}");
            return Task.CompletedTask;
        }

        // for events not implementing CorrelationId
        public Expression<Func<OrderSaga, OrderCancelled, bool>> CorrelationExpression =>
            (saga, @event) => @event.OrderId == saga.CorrelationId;

        public Task Consume(ConsumeContext<OrderCancelled> context)
        {
            Console.WriteLine($"{nameof(OrderSaga)} consume {nameof(OrderCancelled)}: {context.CorrelationId}");
            CancelDate = context.Message.Timestamp;
            return Task.CompletedTask;
        }
    }
}
