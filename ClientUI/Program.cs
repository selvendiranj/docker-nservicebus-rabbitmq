﻿using Messages;
using NServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientUI
{
    public class Program
    {
        static AutoResetEvent closingEvent = new AutoResetEvent(false);

        /// <summary>
        /// Main async method to start, listen and stop endpoint
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            EndpointConfiguration endptConf;
            TransportExtensions<RabbitMQTransport> transport;
            IEndpointInstance endptInst;
            string endptName = "Retail.Docker.ClientUI";
            string recvrName = "Retail.Docker.Sales";
            string rabbitmqh = "host=rabbitmq;RequestedHeartbeat=600";

            Console.CancelKeyPress += OnExit;
            Console.Title = endptName;

            endptConf = new EndpointConfiguration(endptName);
            transport = endptConf.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(rabbitmqh);
            transport.UseConventionalRoutingTopology();
            endptConf.EnableInstallers();

            await Task.Delay(10000); // wait for RabbitMQ to come up
            endptInst = await Endpoint.Start(endptConf).ConfigureAwait(false);

            // Interact with user for sending messages
            await PlaceOrderRequester.RunLoop(endptInst, recvrName).ConfigureAwait(false);

            // Wait until the message arrives.
            closingEvent.WaitOne();

            await endptInst.Stop().ConfigureAwait(false);
        }

        static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            closingEvent.Set();
        }
    }
}
