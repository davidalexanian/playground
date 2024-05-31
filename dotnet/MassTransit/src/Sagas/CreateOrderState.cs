using MassTransit;
using System;

namespace MassTransitProject.Sagas
{
    // Order saga (orchestration example)
    // An instance contains the data for a state machine instance. Compared to consumer sagas it is defined separately
    // A new instance is created for every consumed initial event where an existing instance with the same CorrelationId was not found
    public class CreateOrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public State CurrentState { get; set; }

        public DateTime? SubmitedOn { get; set; }
        
        public bool IsCustomerValid { get; set; }
    }
}
