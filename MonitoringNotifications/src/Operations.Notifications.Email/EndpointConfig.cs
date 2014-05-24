namespace MonitoringNotifications.Operations.Notifications.Email
{
    using CustomSerializer;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public EndpointConfig()
        {
           Configure.Serialization.Adapter();
        }
    }
}
