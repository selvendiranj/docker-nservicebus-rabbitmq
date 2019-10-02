using Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.GUI
{
    public static class Extensions
    {
        public static void AddNServiceBusEndpoint(this IServiceCollection services)
        {
            EndpointConfiguration endpointConfig;
            IEndpointInstance endpointIns;

            Console.Title = "Client.GUI";

            string endpointName = "Client.GUI";
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

            Task.Delay(5000).GetAwaiter().GetResult(); // wait for RabbitMQ to come up
            endpointIns = Endpoint.Start(endpointConfig).GetAwaiter().GetResult();

            services.AddSingleton<IEndpointInstance>(endpointIns);
        }
    }
}
