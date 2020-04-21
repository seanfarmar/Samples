namespace Particular.Shared
{
    using NServiceBus;

    internal class ConfigErrorAndAuditQueues : INeedInitialization
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendFailedMessagesTo("error");
        }
    }
}