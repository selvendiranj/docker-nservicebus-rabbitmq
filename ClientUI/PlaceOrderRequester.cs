using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class PlaceOrderRequester
    {
        /// <summary>
        /// GetLogger for Program
        /// </summary>
        static ILog log = LogManager.GetLogger<Program>();

        /// <summary>
        /// Loop and get input from user, quit when user quits
        /// </summary>
        /// <param name="endptInst"></param>
        /// <returns></returns>
        internal static async Task RunLoop(IEndpointInstance endptInst, string recvrName)
        {
            ConsoleKeyInfo key;
            PlaceOrder command;

            // Instantiate the command
            command = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString()
            };

            // Send the command to the local endpoint
            log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
            await endptInst.Send(recvrName, command).ConfigureAwait(false);

            //while (true)
            //{
            //    log.Info("Press 'P' to place an order, or 'Q' to quit.");

            //    key = Console.ReadKey();
            //    Console.WriteLine();

            //    switch (key.Key)
            //    {
            //        case ConsoleKey.P:
            //            // Instantiate the command
            //            command = new PlaceOrder
            //            {
            //                OrderId = Guid.NewGuid().ToString()
            //            };

            //            // Send the command to the local endpoint
            //            log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
            //            await endptInst.Send(recvrName, command).ConfigureAwait(false);

            //            break;

            //        case ConsoleKey.Q:
            //            return;

            //        default:
            //            log.Info("Unknown input. Please try again.");
            //            break;
            //    }
            //}
        }
    }
}
