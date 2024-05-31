using MassTransit;
using System;

namespace MassTransitProject.Sagas
{
    // A state machine defines the states, events, and behavior of a finite state machine. 
    // (this is example of saga with orchestration)
    public class CreateOrderSaga : MassTransitStateMachine<CreateOrderState>
    {
        // own props

        // states
        public State OrderSubmittedState { get; private set; }
        public State OrderCustomerVerifiedState { get; set; }
        public State OrderCancelledState { get; private set; }
        public State OrderCompletedState { get; private set; }

        // events
        public Event<OrderSubmittedEvent> OrderSubmittedEvent { get; private set; }
        public Event<CreateOrderCustomerVerifiedEvent> CustomerVerified { get; private set; }
        public Event<OrderCancelledEvent> CancelOrderEvent { get; private set; }
        public Event<OrderCompletedEvent> CompleteOrderEvent { get; private set; }

        // to support request/response
        public Event<OrderInformationRequest> OrderInformationRequested { get; private set; }

        static CreateOrderSaga()
        {
            GlobalTopology.Send.UseCorrelationId<OrderSubmittedEvent>(x => x.OrderId);
            GlobalTopology.Send.UseCorrelationId<OrderCancelledEvent>(x => x.OrderId);
            GlobalTopology.Send.UseCorrelationId<OrderCompletedEvent>(x => x.OrderId);
        }

        public CreateOrderSaga()
        {
            // declare property that holds current state
            InstanceState(os => os.CurrentState);

            // define events (an event is a message that can be consumed by the state machine)
            Event(() => OrderSubmittedEvent);
            Event(() => CustomerVerified);
            Event(() => CompleteOrderEvent);
            Event(() => CancelOrderEvent);

            Event(() => OrderInformationRequested, eventConfig => {
                eventConfig.CorrelateById(context => context.Message.OrderId);
                eventConfig.OnMissingInstance(missingInstanceConfig => {
                    return missingInstanceConfig.ExecuteAsync(context => context.RespondAsync(new OrderNotFound { OrderId = context.Message.OrderId }));
                });
            });

            // define flow
            Initially(
                When(OrderSubmittedEvent)
                .Then(ctx => {
                    ctx.Saga.SubmitedOn = DateTime.Now;
                    LogState(ctx.Saga, ctx.Event.Name);
                })
                .TransitionTo(OrderSubmittedState)
                .Publish(context => new CreateOrderVerifyCustomerCommand
                {
                    CustomerName = "some",
                    OrderId = context.Message.OrderId,
                    Timestamp = DateTime.Now,
                }));

            During(OrderSubmittedState,
                When(CustomerVerified, ctx => ctx.Message.IsValidCustomer)
                .Then(ctx =>
                {
                    ctx.Saga.IsCustomerValid = true;
                    LogState(ctx.Saga, ctx.Event.Name);
                })
                .TransitionTo(OrderCustomerVerifiedState),

                When(CustomerVerified, ctx => !ctx.Message.IsValidCustomer)
                .Then(ctx =>
                {
                    ctx.Saga.IsCustomerValid = false;
                    LogState(ctx.Saga, ctx.Event.Name);
                })
                .TransitionTo(OrderCompletedState)
                .Then(ctx => LogState(ctx.Saga, ctx.Event.Name))
                .Finalize());

            DuringAny(
                When(CancelOrderEvent)
                .Then(ctx => LogState(ctx.Saga, ctx.Event.Name))
                .TransitionTo(OrderCompletedState)
                .Finalize());

            // make idempotent
            During(OrderSubmittedState, Ignore(OrderSubmittedEvent));
            During(OrderCustomerVerifiedState, Ignore(OrderSubmittedEvent));
            During(OrderCancelledState, Ignore(OrderSubmittedEvent));
            During(OrderCompletedState, Ignore(CancelOrderEvent));

            // By default, instances are not removed from the saga repository. Use finalizers to remove
            SetCompletedWhenFinalized(); // or custom expression with SetCompleted

            // saga can be configured to reply using request/response pattern
            DuringAny(
                When(OrderInformationRequested)
                .Respond(context => new OrderInformationResponse {
                    OrderId = context.Saga.CorrelationId,
                    State = context.Saga.CurrentState.ToString()
                }));
        }

        private static void LogState(CreateOrderState state, string eventName) =>
            Console.WriteLine($"current state-{state.CurrentState.Name}, event-{eventName}");
    }
}
