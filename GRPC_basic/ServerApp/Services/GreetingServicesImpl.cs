﻿using Greete;
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
    }
}
