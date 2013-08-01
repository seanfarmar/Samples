using NServiceBus;

namespace HalloWorld.MyEndpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client
    {
    }

    public class Unobtrusive : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
                .DefiningCommandsAs(n => n.Namespace != null && n.Namespace.Contains("Commands"))
                .DefiningEventsAs(n => n.Namespace != null && n.Namespace.Contains("Events"));
        }
    }
}