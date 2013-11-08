
namespace RabbitCompeatingConsumers.WCFSender
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, UsingTransport<RabbitMQ>, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder();
        }
    }
}
