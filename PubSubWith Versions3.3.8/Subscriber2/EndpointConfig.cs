namespace Subscriber2
{
	using NServiceBus;

	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefaultBuilder(); // just to show we can mix and match containers
        }
    }
}
