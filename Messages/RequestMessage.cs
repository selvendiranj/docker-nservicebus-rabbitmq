using NServiceBus;
using System;

namespace Messages
{
    public class RequestMessage : IMessage
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
    }
}