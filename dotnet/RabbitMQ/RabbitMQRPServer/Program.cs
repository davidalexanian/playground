using RabbitMQ.Client;
using RabbitMQ.Client.Logging;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
new RabbitMQRPCServer.RPCServer(channel);

Console.WriteLine("RPC Server. Press enter to exit");
Console.ReadLine();