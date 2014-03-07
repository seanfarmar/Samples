
namespace Client
{
    using Messages;
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
	        Configure.With()
	            .DefaultBuilder()
                .DefiningCommandsAs(t =>
	            {
	                var ns = typeof (PriceUpdated).Namespace;
	                return ns != null && (t.Namespace != null && ns.StartsWith(t.Namespace));
	            });
	    }
    }
}
