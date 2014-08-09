namespace NServiceBus.Operations.Notifications
{
    using Features;
    using ServiceControl.Contracts.MessageFailures;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public EndpointConfig()
        {
            Configure.Serialization.Json();

            Configure.Features.Disable<AutoSubscribe>();
        }
    }

    public  class Bootstrap : IWantToRunWhenBusStartsAndStops
    {

        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.Subscribe<MessageFailed>();
        }

        public void Stop()
        {
            Bus.Unsubscribe<MessageFailed>();
        }
    }
}