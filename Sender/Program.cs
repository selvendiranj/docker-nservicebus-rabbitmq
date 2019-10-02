using System;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

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
        RequestMessage message;

        Console.Title = "Sender";

        string endpointName = "Sender";
        string receiverName = "Receiver";
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

        message = new RequestMessage
        {
            Id = Guid.NewGuid(),
            Data = "RequestMessage.Data - String property value"
        };

        Console.WriteLine("Sending a message...");
        Console.WriteLine($"Requesting to get data by id: {message.Id:N}");
        await endpointIns.Send(receiverName, message).ConfigureAwait(false);
        Console.WriteLine("Message sent.");

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await endpointIns.Stop().ConfigureAwait(false);
    }
}