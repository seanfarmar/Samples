namespace Server
{
    using System;
    using System.Configuration;
    using Messages;
    using NServiceBus;
    using NServiceBus.Support;

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
                    var ns = typeof(PriceUpdated).Namespace;
                    return ns != null && (t.Namespace != null && ns.StartsWith(t.Namespace));
                })
                .SetEndpointSLA(new TimeSpan(0, 0, int.Parse(ConfigurationManager.AppSettings["SLAInSeconds"])))
                .UseTransport<Msmq>()
                .DisableGateway()
                .DisableTimeoutManager();

           var customMachineName = ConfigurationManager.AppSettings["CustomMachineName"];

            if (!string.IsNullOrEmpty(customMachineName))
            {
                RuntimeEnvironment.MachineNameAction = () => customMachineName;
            }
	    }
    }
}
