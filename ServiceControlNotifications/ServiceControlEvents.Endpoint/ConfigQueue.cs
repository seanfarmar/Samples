namespace ServiceControlEvents.Endpoint
{
    using NServiceBus;

    internal class ConfigErrorAndAuditQueues : INeedInitialization
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.AuditProcessedMessagesTo("audit2");
            endpointConfiguration.SendFailedMessagesTo("error2");
        }
    }
}