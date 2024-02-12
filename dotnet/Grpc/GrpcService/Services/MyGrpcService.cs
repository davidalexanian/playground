using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class MyGrpcService : GrpcService.ExampleService.ExampleServiceBase
    {
        private readonly ILogger<MyGrpcService> _logger;
        public MyGrpcService(ILogger<MyGrpcService> logger)
        {
            _logger = logger;
        }

        public override Task<ExampleResponse> UnaryCall(ExampleRequest request, ServerCallContext context) =>
            Task.FromResult(new ExampleResponse { Message = $"Page {request.PageSize} {request.PageIndex}" });

        public override async Task StreamingFromServer(ExampleRequest request, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
        {
            var start = (request.PageIndex - 1) * request.PageSize;
            start = start == 0 ? 1 : start;
            
            await foreach (var item in RangeAsync(start, request.PageSize).WithCancellation(context.CancellationToken)) {
                await responseStream.WriteAsync(new ExampleResponse { Message = item });
            }
        }

        public override async Task<ExampleResponse> StreamingFromClient(IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
        {
            int count = 0;
            while (await requestStream.MoveNext()) {
                _logger.LogInformation($"Client data received: {requestStream.Current.PageIndex}, {requestStream.Current.PageSize}");
                count++;
            }
            return new ExampleResponse { Message = $"{count} messages received" };
        }

        public override async Task StreamingBothWays(IAsyncStreamReader<ExampleRequest> requestStream, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
        {
            var readerTask = Task.Run(async () => {
                int count = 0;
                while (await requestStream.MoveNext()) {
                    _logger.LogInformation($"Client data received: {requestStream.Current.PageIndex}, {requestStream.Current.PageSize}");
                    count++;
                }
                return new ExampleResponse { Message = $"{count} messages received" };
            });

            var writerTask = Task.Run(async () => {
                await foreach (var item in RangeAsync(1, 10)) {
                    await responseStream.WriteAsync(new ExampleResponse { Message = item });
                    _logger.LogInformation($"Server sent data: {item}");
                }
            });
            await readerTask;
        }

        static async IAsyncEnumerable<string> RangeAsync(int start, int count)
        {
            for (int i = start; i < count; i++)
            {
                await Task.Delay(500);
                yield return $"Message {i}";
            }
        }
    }
}