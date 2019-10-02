using NServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping
{
    public class Program
    {
        /// <summary>
        /// Main async method to start, listen and stop endpoint
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            EndpointConfiguration endpointConfig;
            IEndpointInstance endpointIns;

            Console.Title = "Shipping";

            string endpointName = "Shipping";
            string rabbitmqhost = "host=rabbitmq;RequestedHeartbeat=600";
            string errorQueue = "error";
            string auditQueue = "audit";
            string serviceControlQueue = "Particular.ServiceControl";
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
