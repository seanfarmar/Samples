namespace HalloWorld.Unobtrusive
{
    using NServiceBus;

    public class Configuration : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
                .DefiningCommandsAs(n => n.Namespace != null && n.Namespace.Contains("Commands"))
                .DefiningEventsAs(n => n.Namespace != null && n.Namespace.Contains("Events"));
        }
    }
}
