namespace WCFHosting.CustomHost
{
    using System;
    using System.Diagnostics;
    using System.ServiceModel;
    using NServiceBus;

    public class NsbHost : ServiceHost
    {
        public static IBus Bus;

        public NsbHost(Type t, params Uri[] baseAddresses) : base(t, baseAddresses)
        {
        }

        protected override void OnOpening()
        {
            var configuration = new BusConfiguration();
            configuration.PurgeOnStartup(true);

            if (Debugger.IsAttached)
            {
                configuration.UsePersistence<InMemoryPersistence>();
            }

            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("WCFHosting") &&
                                         t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("WCFHosting") &&
                                       t.Namespace.EndsWith("Events"));

            // In Production, make sure the necessary queues for this endpoint are installed before running
            if (Debugger.IsAttached)
            {
                // While calling this code will create the necessary queues required, this step should
                // ideally be done just one time as opposed to every execution of this endpoint.
                configuration.EnableInstallers();
            }

            Bus = NServiceBus.Bus.Create(configuration).Start();

            base.OnOpening();
        }
    }
}