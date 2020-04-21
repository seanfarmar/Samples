namespace Particular.Shared
{
    using NServiceBus;
    using System;
    using System.Configuration;

    public static class CommonWebConfiguration
    {
        const int MaxEntityName = 50;

        public static void ApplyWebEndpointConfiguration(this EndpointConfiguration endpointConfiguration,
            Action<TransportExtensions<RabbitMQTransport>> messageEndpointMappings = null)
        {
            endpointConfiguration
                .LicensePath(AppDomain.CurrentDomain.BaseDirectory + @"\License.xml");

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(ConfigurationManager.AppSettings.Get("NServiceBusConnectionString"));

            var metrics = endpointConfiguration.EnableMetrics();
            metrics.SendMetricDataToServiceControl(
                serviceControlMetricsAddress: "Particular.Monitoring",
                interval: TimeSpan.FromSeconds(2));

            endpointConfiguration.SendHeartbeatTo(
                serviceControlQueue: "Particular.ServiceControl",
                frequency: TimeSpan.FromSeconds(15),
                timeToLive: TimeSpan.FromSeconds(30));

            messageEndpointMappings?.Invoke(transport);
        }
    }
}