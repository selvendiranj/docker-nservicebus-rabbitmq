using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Billing has received OrderPlaced, OrderId = {message.OrderId}");

            var orderBilled = new OrderBilled
            {
                OrderId = message.OrderId
            };

            return context.Publish(orderBilled);

            return Task.CompletedTask;
        }
    }
}