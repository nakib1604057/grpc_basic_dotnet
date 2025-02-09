using Greete;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class ClientService
    {
        const string Target = "127.0.0.1:5100";
        public static void Main(string[] args)
        {
            Console.WriteLine("hello from client");
            // The port number must match the port of the gRPC server.
            Channel channel = new Channel(Target, ChannelCredentials.Insecure);
            channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Client connected to the server");
                }
            });
            var client = new Greeter.GreeterClient(channel);
            var response = client.GreeterService(new HelloRequest()
            {
                Name = "Akram",
            });
            Console.WriteLine(response.Message);
            channel.ShutdownAsync().Wait();
        }
    }
}
