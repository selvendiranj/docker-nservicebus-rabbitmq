using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

public class RequestMessageHandler : IHandleMessages<RequestMessage>
{
    static ILog log = LogManager.GetLogger<RequestMessageHandler>();
    ResponseMessage response;

    public Task Handle(RequestMessage message, IMessageHandlerContext context)
    {
        log.Info($"Request received with Id: {message.Id}, description: {message.Data}");

        response = new ResponseMessage
        {
            Id = message.Id,
            Data = message.Data + " - request has been processed"
        };

        return context.Reply(response);
    }
}