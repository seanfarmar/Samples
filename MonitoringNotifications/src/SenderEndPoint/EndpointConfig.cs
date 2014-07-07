namespace MonitoringNotifications.SenderEndPoint
{
    using CustomSerializer;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public EndpointConfig()
        {
            Configure.Serialization.Json();
            //Configure.Serialization.Xml();
            //Configure.Serialization.Adapter();
        }
    }
}
