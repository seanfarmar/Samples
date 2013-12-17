namespace TestServer
{
    using NServiceBus;

    public class EndpointConfig : AsA_Server, IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .FtpTransport()
                .UnicastBus()
                .LoadMessageHandlers();
        }
    }
}
