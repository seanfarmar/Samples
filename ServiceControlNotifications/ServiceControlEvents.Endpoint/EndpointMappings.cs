namespace ServiceControlEvents.Endpoint
{
    using System;
    using System.Configuration;
    using NServiceBus;
    using ServiceControl.Contracts;

    public class EndpointMappings
    {
        internal static Action<TransportExtensions<AzureServiceBusTransport>> MessageEndpointMappings()
        {
            return transport =>
            {
                var transportSettings = transport.UseEndpointOrientedTopology();

                // events
                transportSettings.RegisterPublisher(typeof(MessageFailed).Assembly,
                    ConfigurationManager.AppSettings["ServiceControl/Queue"]);
            };
        }
    }
}