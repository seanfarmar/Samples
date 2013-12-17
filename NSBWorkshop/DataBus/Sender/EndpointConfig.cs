namespace Sender
{
	using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client, IWantCustomInitialization
	{
		public static string BasePath = "..\\..\\..\\storage";

		public void Init()
		{
		    Configure.With()
                .DefaultBuilder()
		        .FileShareDataBus(BasePath)
		        .UnicastBus();
		}
	}
}
