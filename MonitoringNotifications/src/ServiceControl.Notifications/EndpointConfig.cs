namespace MonitoringNotifications.ServiceControl.Notifications
{
    using CustomSerializer;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint,AsA_Server
    {
        public EndpointConfig()
        {
            Configure.Serialization.Adapter();
        }
    }
}
