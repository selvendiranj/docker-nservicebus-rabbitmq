using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class DoSomethingHandler : IHandleMessages<DoSomething>
    {
        public async Task Handle(DoSomething message, IMessageHandlerContext context)
        {
            // Do something with the message here
            Console.WriteLine("Received DoSomething");
            await Task.CompletedTask;
        }
    }

    public class DoSomethingElseHandler : IHandleMessages<DoSomething>, IHandleMessages<DoSomethingElse>
    {
        public Task Handle(DoSomething message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received DoSomething");
            return Task.CompletedTask;
        }

        public Task Handle(DoSomethingElse message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received DoSomethingElse");
            return Task.CompletedTask;
        }
    }
}
