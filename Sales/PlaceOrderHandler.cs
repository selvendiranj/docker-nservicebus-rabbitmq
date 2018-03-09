using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        static Random random = new Random();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            OrderPlaced orderPlaced;

            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            //return Task.CompletedTask;

            // This is normally where some business logic would occur

            // throw new Exception("BOOM");
            // example to explain retries
            //if (random.Next(0, 5) == 0)
            //{
            //    throw new Exception("Oops");
            //}

            orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };

            return context.Publish(orderPlaced);
        }
    }
}
