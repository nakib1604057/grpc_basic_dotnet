using Greete;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Services
{
    public class GreetingServicesImpl: Greete.Greeter.GreeterBase
    {
        public override Task<HelloReply> GreeterService(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply()
            {
                Message = "Hello from server",
            });
        }
        public override async Task GreeteServerStream(GreetManyTimeRequest request, IServerStreamWriter<GreetManyTimeResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Request from client: {request.Message}");
            foreach (int i in Enumerable.Range(0, 10))
            {
                await responseStream.WriteAsync(new GreetManyTimeResponse()
                {
                    Message = i.ToString(),
                });
            }
        }
        public override async Task<GreetManyTimeResponse> GreeteClientStream(IAsyncStreamReader<GreetManyTimeRequest> requestStream, ServerCallContext context)
        {
            Console.WriteLine($"Request from client");
            int sum = 0;
            while(await requestStream.MoveNext())
            {
                sum += Convert.ToInt32(requestStream.Current.Message);
            }
            return new GreetManyTimeResponse()
            {
                Message = sum.ToString(),
            };
        }
    }
}
