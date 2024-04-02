using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MassTransitProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            string exchange = "test.fanout";
            string queue1 = nameof(queue1);
            string queue2 = nameof(queue2);
            string routingKey = string.Empty;   //nameof(routingKey);

            channel.ExchangeDeclare(exchange, "fanout", durable: true);
            channel.QueueDeclare(queue: queue1, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: queue2, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue1, exchange, routingKey, arguments: null);
            channel.QueueBind(queue2, exchange, routingKey, arguments: null);
            RegisterConsumer(channel, "consumer1", queue1);
            RegisterConsumer(channel, "consumer2", queue2);
            DirectSend(channel, exchange, routingKey, 1);

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        static void DirectSend(IModel channel, string exchangeName, string routingKey, int count)
        {
            for(int counter = 1; counter <= count; counter++) {
                var message = $"message-{counter}";
                channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, body: Encoding.UTF8.GetBytes(message));
                Console.WriteLine($"Sent '{message}' message to '{exchangeName}' exchange.");
            }
        }

        static void RegisterConsumer(IModel channel, string consumerName, string queueName)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) => {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"Consumer {consumerName} received '{message}' message.");
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}