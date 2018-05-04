namespace WebServiceIntegration.ITOps
{
    using System;
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.Persistence;

    public static class EndpointConfigurationExtensions
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(EndpointConfigurationExtensions));

        public static EndpointConfiguration Configure(
            this EndpointConfiguration endpointConfiguration,
            string connectionString,
            Action<RoutingSettings<MsmqTransport>> configureRouting)
        {
            NServiceBus.Logging.LogManager.Use<DefaultFactory>();
            Log.Info("Configuring endpoint...");

            // var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\License.xml");
            // endpointConfiguration.LicensePath(licensePath);

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));

            var routing = endpointConfiguration.UseTransport<MsmqTransport>()
                .ConnectionString("deadLetter=false;journal=false")
                .Routing();

            endpointConfiguration.UsePersistence<NHibernatePersistence>().ConnectionString(connectionString);

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.Conventions()
                .DefiningCommandsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("WebServiceIntegration.Messages") &&
                    t.Namespace.EndsWith("Response"));

            endpointConfiguration.EnableInstallers();

            configureRouting?.Invoke(routing);

            return endpointConfiguration;           
        }        
    }
}