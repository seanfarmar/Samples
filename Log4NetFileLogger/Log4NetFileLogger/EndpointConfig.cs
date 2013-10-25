namespace Log4NetFileLogger 
{
    using NServiceBus;

	public class EndpointConfig : IConfigureThisEndpoint, IWantCustomLogging, AsA_Server
    {
	    public void Init()
	    {
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
	    }
    }
}