namespace WebServiceIntegration.Conventions
{
    using NServiceBus;

    internal class UnobtrusiveMessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
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