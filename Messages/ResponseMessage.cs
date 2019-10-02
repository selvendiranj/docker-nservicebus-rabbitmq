using NServiceBus;
using System;

namespace Messages
{
    public class ResponseMessage : IMessage
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
    }
}