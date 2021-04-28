using System;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

class Program
{
    static async Task Main()
    {
        Console.Title = "NotificationsEndpoint";

        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Fatal);

        #region endpointConfig

        var endpointConfiguration = new EndpointConfiguration("NotificationsEndpoint");
        await SubscribeToNotifications.Subscribe(endpointConfiguration)
            .ConfigureAwait(false);

        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.UseTransport<LearningTransport>();

        #endregion

        #region customDelayedRetries

        var recoverability = endpointConfiguration.Recoverability();
        recoverability.Delayed(
            customizations: delayed =>
            {
                delayed.TimeIncrease(TimeSpan.FromSeconds(1));
            });

        #endregion

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        var message = new MyMessage
        {
            Property = "PropertyValue"
        };

        await endpointInstance.SendLocal(message)
            .ConfigureAwait(false);

        Console.WriteLine("Press any key to exit");

        Console.ReadKey();

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}