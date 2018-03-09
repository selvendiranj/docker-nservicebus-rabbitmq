using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static AutoResetEvent closingEvent = new AutoResetEvent(false);

    static async Task Main()
    {
        EndpointConfiguration endptConf;
        TransportExtensions<RabbitMQTransport> transport;
        IEndpointInstance endptInst;
        string endptName = "Retail.Docker.Sender";
        string recvrName = "Retail.Docker.Receiver";
        string rabbitmqh = "host=rabbitmq";
        RequestMessage message;
        
        Console.CancelKeyPress += OnExit;
        Console.Title = endptName;

        endptConf = new EndpointConfiguration(endptName);
        transport = endptConf.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitmqh);
        transport.UseConventionalRoutingTopology();
        endptConf.EnableInstallers();

        endptInst = await Endpoint.Start(endptConf).ConfigureAwait(false);

        message = new RequestMessage
        {
            Id = Guid.NewGuid(),
            Data = "RequestMessage.Data - String property value"
        };

        Console.WriteLine("Sending a message...");
        Console.WriteLine($"Requesting to get data by id: {message.Id:N}");

        await endptInst.Send(recvrName, message).ConfigureAwait(false);

        Console.WriteLine("Message sent.");
        Console.WriteLine("Use 'docker-compose down' to stop containers.");

        // Wait until the message arrives.
        closingEvent.WaitOne();

        await endptInst.Stop().ConfigureAwait(false);
    }

    static void OnExit(object sender, ConsoleCancelEventArgs args)
    {
        closingEvent.Set();
    }
}