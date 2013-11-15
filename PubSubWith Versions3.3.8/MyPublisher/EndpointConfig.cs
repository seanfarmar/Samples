using NServiceBus;

namespace MyPublisher
{
    class EndpointConfig :  IConfigureThisEndpoint, AsA_Publisher,IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
				.MsmqSubscriptionStorage();
		}
    }
}
