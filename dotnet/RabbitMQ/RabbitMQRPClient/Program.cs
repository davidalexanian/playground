Console.WriteLine("RPC CLient. Press ESC to exit or ENTER to send request");

var client = new RabbitMQRPCClient.RpcClient();

int i = 0;
while (Console.ReadKey().Key != ConsoleKey.Escape) {
    var message = $"request {++i}";
    var result = await client.CallAsync(message);
    Console.WriteLine(result);
}
