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
        string endptName = "Retail.Docker.Receiver";
        string rabbitmqh = "host=rabbitmq";

        Console.CancelKeyPress += OnExit;
        Console.Title = endptName;

        endptConf = new EndpointConfiguration(endptName);

        #region TransportConfiguration

        transport = endptConf.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitmqh);
        transport.UseConventionalRoutingTopology();

        #endregion

        endptConf.EnableInstallers();

        endptInst = await Endpoint.Start(endptConf).ConfigureAwait(false);

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