using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            OrderBilled orderBilled;

            log.Info($"Received OrderPlaced, OrderId = {message.OrderId} - Charging credit card...");

            orderBilled = new OrderBilled
            {
                OrderId = message.OrderId
            };

            return context.Publish(orderBilled);
        }
    }
}
