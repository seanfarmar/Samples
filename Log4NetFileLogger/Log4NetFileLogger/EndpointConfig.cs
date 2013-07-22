namespace Log4NetFileLogger 
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, IWantCustomLogging, AsA_Server
    {
	    public void Init()
	    {
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
	    }
    }
}