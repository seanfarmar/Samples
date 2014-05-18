namespace MonitoringNotifications.Operations.Notifications.Email
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint,AsA_Server
    {
        public EndpointConfig()
        {
            Configure.Serialization.Json();
        }
    }
}
