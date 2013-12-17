namespace Frontend
{
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Installation.Environments;

    public static class ServiceBus
    {
        public static IBus Bus { get; private set; }
        public static void Init() {
            if (Bus != null) return;

            lock (typeof (ServiceBus)) {
                if (Bus != null) return;

                NServiceBus.Configure.Features.Enable<Sagas>();

                Bus = Configure.With()
                    .DefineEndpointName("Frontend")
                    .DefaultBuilder()
                    //.MsmqTransport() // NSB 3.x
                    .UseTransport<Msmq>() // NSB 4.x
                    .RavenSubscriptionStorage()
                    //.DBSubcriptionStorage()
                    //.IsTransactional(true)
                    .PurgeOnStartup(true)
                    .UnicastBus()
                    //.Sagas()
                    //.NHibernateSagaPersister()
                    .CreateBus()
                    //.Start(); // v3.x
                    .Start(() => Configure.Instance  //v4.x
                                          .ForInstallationOn<Windows>()
                                          .Install());
            }
         }
    }
}
