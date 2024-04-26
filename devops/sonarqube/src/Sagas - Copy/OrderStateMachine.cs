using MassTransit;
using System;

namespace SonarqubeSampleProject.Sagas
{

    // A state machine defines the states, events, and behavior of a finite state machine. 
    // Created once, and then used to apply event triggered behavior to state machine instances.
    public class OrderStateMachineCopy : MassTransitStateMachine<OrderState>
    {
        // own props

        // states
        public State OrderSubmittedState { get; private set; }
        public State OrderCancelledState { get; private set; }
        public State OrderCompletedState { get; private set; }

        // events
        public Event<OrderSubmittedCommand> SubmitOrderEvent { get; private set; }
        public Event<OrderCancelledCommand> CancelOrderEvent { get; private set; }
        public Event<OrderCompletedCommand> CompleteOrderEvent { get; private set; }

        // to support request/response
        public Event<OrderInformationRequest> OrderInformationRequested { get; private set; }

        static OrderStateMachineCopy()
        {
            GlobalTopology.Send.UseCorrelationId<OrderSubmittedCommand>(x => x.OrderId);
            GlobalTopology.Send.UseCorrelationId<OrderCancelledCommand>(x => x.OrderId);
            GlobalTopology.Send.UseCorrelationId<OrderCompletedCommand>(x => x.OrderId);
        }

        public OrderStateMachineCopy()
        {
            // declare property that holds current state
            InstanceState(os => os.CurrentState);

            // events (an event is a message that can be consumed by the state machine)
            Event(() => SubmitOrderEvent);            
            Event(() => CompleteOrderEvent, (eventConfig) => {
                //eventConfig.CorrelateById(context => context.Message.OrderId);
                eventConfig.OnMissingInstance(missingInstanceConfig => {
                    return missingInstanceConfig.ExecuteAsync(context => context.RespondAsync(new OrderNotFound { OrderId = context.Message.OrderId }));
                });
            });
            Event(() => CancelOrderEvent);
            Event(() => OrderInformationRequested, eventConfig => {
                eventConfig.CorrelateById(context => context.Message.OrderId);
            });

            //// define transitions
            // declare events,activities handled in inital state
            Initially(
                When(SubmitOrderEvent)
                .Then(ctx => {
                    ctx.Saga.SubmitedOn = DateTime.Now;
                    Console.WriteLine($"{nameof(OrderStateMachine)}: current state-{ctx.Saga.CurrentState.Name}, event-{ctx.Event}");
                })
                .TransitionTo(OrderSubmittedState)
                .Publish(context => new OrderSubmitted { OrderId = context.Saga.CorrelationId, Timestamp = DateTime.Now }));  // saga can publish messages on state change

            // declare events,activities handled during specified state
            During(OrderSubmittedState,
                When(CompleteOrderEvent)
                .Then(ctx => Console.WriteLine($"{nameof(OrderStateMachine)}: current state-{ctx.Saga.CurrentState.Name}, event-{ctx.Event}"))
                .TransitionTo(OrderCompletedState)); ;
            
            During(OrderSubmittedState, 
                When(CancelOrderEvent)
                .Then(ctx => Console.WriteLine($"{nameof(OrderStateMachine)}: current state-{ctx.Saga.CurrentState.Name}, event-{ctx.Event}"))
                .TransitionTo(OrderCancelledState));

            During(OrderCompletedState,
                When(CancelOrderEvent)
                .Then(ctx => Console.WriteLine($"{nameof(OrderStateMachine)}: current state-{ctx.Saga.CurrentState.Name}, event-{ctx.Event}"))
                .TransitionTo(OrderCancelledState)); ;

            During(OrderSubmittedState, Ignore(SubmitOrderEvent));
            During(OrderCompletedState, Ignore(SubmitOrderEvent));
            During(OrderCompletedState, Ignore(CancelOrderEvent));

            // By default, instances are not removed from the saga repository. Use finalizers to remove
            DuringAny(When(CancelOrderEvent).Finalize()); 
            SetCompletedWhenFinalized(); // or custom expression with SetCompleted

            // saga can be configured to reply using request/response pattern
            DuringAny(
                When(OrderInformationRequested)
                .Respond(context => new OrderInformationResponse { 
                    OrderId = context.Saga.CorrelationId, 
                    State = context.Saga.CurrentState.ToString() }));
        }
    }
}
