using NServiceBus;

namespace HalloWorld.MyEndpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomLogging, AsA_Publisher
    {
        public void Init()
	    {
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);

            Configure.With().Log4Net();
	    }
    }
}