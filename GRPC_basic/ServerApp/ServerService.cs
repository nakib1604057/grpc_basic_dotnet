using Grpc.Core;
using ServerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class ServerService
    {
        const int Port = 5100;
        public static void Main(string[] args)
        {
            Server server = new Server()
            {
                Services = {Greete.Greeter.BindService(new GreetingServicesImpl())},
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) },
            };
            server.Start();
            Console.WriteLine($"Server Started at port: {Port}..........");
            Console.ReadLine();
            server.ShutdownAsync();
        }
    }
}
