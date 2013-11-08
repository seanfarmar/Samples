namespace RabbitCompetingConsumers.Subscriber
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization, AsA_Server, UsingTransport<RabbitMQ>
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder();
        }
    }
}
