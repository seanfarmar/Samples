using NServiceBus;

namespace MyPublisher
{
    class EndpointConfig :  IConfigureThisEndpoint, AsA_Publisher,IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .SpringFrameworkBuilder()
                //this overrides the NServiceBus default convention of IEvent
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("MyMessages"));}
    }

    public class CustomConfig
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .DBSubcriptionStorage();
        }
    }
}
