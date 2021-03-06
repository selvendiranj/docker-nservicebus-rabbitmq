﻿using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Sales
{
    class Program
    {
        /// <summary>
        /// Main async method to start, listen and stop endpoint
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            EndpointConfiguration endpointConfig;
            IEndpointInstance endpointIns;

            Console.Title = "Sales";

            string endpointName = "Sales";
            string rabbitmqhost = "host=rabbitmq;RequestedHeartbeat=600";
            string errorQueue = "error";
            string auditQueue = "audit";
            string serviceControlQueue = "Particular.Servicecontrol";
            string serviceControlMetricsAddress = "Particular.Monitoring";

            endpointConfig = new EndpointConfiguration(endpointName);
            endpointConfig.UseTransport<RabbitMQTransport>()
                          .ConnectionString(rabbitmqhost)
                          .UseConventionalRoutingTopology();
            endpointConfig.EnableInstallers();

            endpointConfig.SendFailedMessagesTo(errorQueue);
            endpointConfig.AuditProcessedMessagesTo(auditQueue);
            endpointConfig.SendHeartbeatTo(serviceControlQueue);

            var metrics = endpointConfig.EnableMetrics();
            metrics.SendMetricDataToServiceControl(serviceControlMetricsAddress, TimeSpan.FromMilliseconds(500));

            await Task.Delay(5000); // wait for RabbitMQ to come up
            endpointIns = await Endpoint.Start(endpointConfig).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointIns.Stop().ConfigureAwait(false);
        }
    }
}