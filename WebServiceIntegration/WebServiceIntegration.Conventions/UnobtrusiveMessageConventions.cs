namespace WebServiceIntegration.Conventions
{
    using NServiceBus;

    internal class UnobtrusiveMessageConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Response"));
        }
    }
}