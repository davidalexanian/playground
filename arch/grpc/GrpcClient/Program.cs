using Grpc.Net.Client;
using GrpcClient;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

using var channel = GrpcChannel.ForAddress("https://localhost:7280");
var client = new ExampleService.ExampleServiceClient(channel);
var cts = new CancellationTokenSource();

Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => {
    Console.WriteLine("Cancellation requested");
    cts.Cancel();
};

while (true)
{
    Console.WriteLine("\nType number for presenting\n");
    Console.WriteLine("1 - unary request");
    Console.WriteLine("2 - server stream");
    Console.WriteLine("3 - client stream");
    Console.WriteLine("4 - both stream");

    var input = Console.ReadLine();
    switch (input)
    {
        case "1":   // unary request/response
            var response1 = await client.UnaryCallAsync(new ExampleRequest { PageIndex = 1, PageSize = 2 });
            Console.WriteLine(response1.Message);
            break;

        case "2" :  // stream from server
            var response2 = client.StreamingFromServer(new ExampleRequest { PageIndex = 1, PageSize = 10 });
            while (await response2.ResponseStream.MoveNext(CancellationToken.None)) {
                Console.WriteLine(response2.ResponseStream.Current.Message);
            }
            break;

        case "3":   // stream from client
            var metadata = new Grpc.Core.Metadata();
            metadata.Add("key1", "value1");
            metadata.Add("key2", "value2");
            var clientStreamingCall = client.StreamingFromClient(metadata, null, cts.Token);
            for (int i = 1; i <= 10; i++) {
                await clientStreamingCall.RequestStream.WriteAsync(new ExampleRequest { PageIndex = i, PageSize = 10 }, cts.Token);
                await Task.Delay(500);
            }
            await clientStreamingCall.RequestStream.CompleteAsync();
            var callResponse = await clientStreamingCall;
            Console.WriteLine($"Client streaming completed. Message from server: {callResponse.Message}");
            break;

        case "4":   // stream both ways
            var call = client.StreamingBothWays(cancellationToken: cts.Token);
            
            var readerTask = Task.Run(async () => {
                while (await call.ResponseStream.MoveNext(cts.Token)) {
                    Console.WriteLine($"Server response: {call.ResponseStream.Current.Message}");
                }
            });
            
            var senderTask = Task.Run(async () => {
                for (int i = 1; i <= 10; i++) {
                    await call.RequestStream.WriteAsync(new ExampleRequest { PageIndex = i, PageSize = 10 }, cts.Token);
                    Console.WriteLine($"Client sent data {i}");
                    await Task.Delay(500);
                }
                await call.RequestStream.CompleteAsync();
            });
            await readerTask;
            break;

        default:
            Console.WriteLine("Invalid input\n");
            break;
    }

}
