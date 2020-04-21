namespace Particular.Shared
{
    using NServiceBus;

    internal class UnobtrusiveMessageConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            var conventions = configuration.Conventions();

            conventions.DefiningMessagesAs(t =>
                t.Namespace != null && t.Namespace.StartsWith("") &&
                (t.Namespace.EndsWith("Messages")) || t.Namespace != null && t.Namespace.EndsWith("Replies"));

            conventions.DefiningCommandsAs(t =>
                t.Namespace != null && t.Namespace.StartsWith("") &&
                t.Namespace.EndsWith("Commands"));

            conventions.DefiningEventsAs(t =>
                t.Namespace != null && t.Namespace.StartsWith("") &&
                t.Namespace.EndsWith("Events"));
        }
    }
}