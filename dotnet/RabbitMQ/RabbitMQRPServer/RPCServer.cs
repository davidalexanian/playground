using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQRPCServer
{
    public class RPCServer
    {
        public RPCServer(IModel channel)
        {
            channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);     // allow only one handler
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);

            consumer.Received += (object? sender, BasicDeliverEventArgs ea) => {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"RPC Server request recieved: {message}");

                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = ea.BasicProperties.CorrelationId;    // this is needed for client
                channel.BasicPublish(exchange: string.Empty, 
                    routingKey: ea.BasicProperties.ReplyTo,
                    basicProperties: replyProps,
                    body: Encoding.UTF8.GetBytes($"response to '{message}'"));

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
        }
    }
}