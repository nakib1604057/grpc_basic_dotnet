using Greete;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Channel = Grpc.Core.Channel;

namespace ClientApp
{
    public class ClientService
    {
        const string Target = "127.0.0.1:5100";
        public static async Task Main(string[] args)
        {
            Console.WriteLine("hello from client");
            // The port number must match the port of the gRPC server.
            Channel channel = new Channel(Target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Client connected to the server");
                }
            });
            //await ServerStreaming(channel);
            //GreetingResponse(channel);
            await ClientStreaming(channel);
            channel.ShutdownAsync().Wait();

        }
        public static async Task GreetingResponse(Channel channel)
        {
            var client = new Greeter.GreeterClient(channel);
            var response = client.GreeterService(new HelloRequest()
            {
                Name = "Akram",
            });
            Console.WriteLine(response.Message);
        }
        public static async Task ServerStreaming(Channel channel)
        {
            var client = new Greeter.GreeterClient(channel);
            var response = client.GreeteServerStream(new GreetManyTimeRequest()
            {
                Message = "Streaming Request From server"
            });
            while(await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Message);
            }
        }
        public static async Task ClientStreaming(Channel channel)
        {
            var client = new Greeter.GreeterClient(channel);
            var streaming = client.GreeteClientStream();
            foreach(var i in Enumerable.Range(0, 10))
            {
                await streaming.RequestStream.WriteAsync(new GreetManyTimeRequest()
                {
                    Message = i.ToString()
                });
            }
            await streaming.RequestStream.CompleteAsync();
            var response = await streaming.ResponseAsync;
            Console.WriteLine(response.Message);
        }
    }
}
