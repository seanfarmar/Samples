namespace ServiceControlEvents.Endpoint
{
    using System;
    using Microsoft.Azure;
    using NServiceBus;
    using NServiceBus.Logging;

    public static class PrivateConfiguration
    {
        public static void ApplyEndpointConfiguration(this EndpointConfiguration endpointConfiguration,
            Action<TransportExtensions<AzureServiceBusTransport>> messageEndpointMappings = null)
        {
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(CloudConfigurationManager.GetSetting("NServiceBusConnectionString"));

            var sanitization = transport.Sanitization();
            sanitization.UseSubscriptionPathMaximumLength(256);

            messageEndpointMappings?.Invoke(transport);

            var persistance = endpointConfiguration.UsePersistence<AzureStoragePersistence>();
            persistance.ConnectionString(CloudConfigurationManager.GetSetting("PrimaryStorageConnectionString"));

            #region DisableRetries

            var recoverability = endpointConfiguration.Recoverability();

            recoverability.Delayed(
                retriesSettings => { retriesSettings.NumberOfRetries(0); });
            recoverability.Immediate(
                retriesSettings => { retriesSettings.NumberOfRetries(3); });

            #endregion

            endpointConfiguration.EnableInstallers();

            LogManager.Use<DefaultFactory>().Level(LogLevel.Debug);

            // https://docs.particular.net/servicecontrol/contracts
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(type => typeof(IEvent).IsAssignableFrom(type) ||
                                                 type.Namespace != null &&
                                                 type.Namespace.StartsWith("ServiceControl.Contracts"));

            conventions.DefiningMessagesAs(t =>
                t.Namespace != null && t.Namespace.StartsWith("ServiceControlEvents.") &&
                t.Namespace.EndsWith("Messages") || t.Namespace != null && t.Namespace.EndsWith("Replies"));

            conventions.DefiningCommandsAs(t =>
                t.Namespace != null && t.Namespace.StartsWith("ServiceControlEvents.") &&
                t.Namespace.EndsWith("Commands"));

            endpointConfiguration.AuditProcessedMessagesTo("audit2");
            endpointConfiguration.SendFailedMessagesTo("error2");
        }
    }
}