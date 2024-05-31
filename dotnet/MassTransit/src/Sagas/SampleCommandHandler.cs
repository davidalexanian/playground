using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Transports;

namespace MassTransitProject.Sagas
{
    // both will get or none will get the command
    public class CreateOrderVerifyCustomerCommandHandler : IConsumer<CreateOrderVerifyCustomerCommand>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public CreateOrderVerifyCustomerCommandHandler(IPublishEndpoint publishEndpoint) 
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CreateOrderVerifyCustomerCommand> context)
        {
            Console.WriteLine($"{nameof(CreateOrderVerifyCustomerCommandHandler)}: {context.Message}");
            
            var endpoint = await context.ReceiveContext.PublishEndpointProvider.GetPublishSendEndpoint<CreateOrderCustomerVerifiedEvent>();
            var response = new CreateOrderCustomerVerifiedEvent
            {
                IsValidCustomer = !string.IsNullOrEmpty(context.Message.CustomerName),
                OrderId = context.Message.OrderId,
                Timestamp = DateTime.Now
            };
            await endpoint.Send(response);
            // or await publishEndpoint.Publish(response);
        }
    }
}
