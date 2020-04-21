namespace SomeEndpoint
{
    using Messages.Commands;
    using NServiceBus;
    using System;

    public class EndpointMappings
    {
        internal static Action<TransportExtensions<RabbitMQTransport>> MessageEndpointMappings()
        {
            return transport =>
            {
                var routing = transport.Routing();
                routing.RouteToEndpoint(typeof(SendSmsMessage), "Notification");
                routing.RouteToEndpoint(typeof(SendSmsMessageMessageBird), "Notification");
                routing.RouteToEndpoint(typeof(SendSmsMessageTwilio), "Notification");
                routing.RouteToEndpoint(typeof(SendSmtpEmail), "Notification");
            };
        }
    }
}