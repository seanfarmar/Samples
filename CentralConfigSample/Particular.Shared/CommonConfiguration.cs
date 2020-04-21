namespace Particular.Shared
{
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.Serilog;
    using Serilog;
    using Serilog.Exceptions;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public static class CommonConfiguration
    {
        public static void ApplyEndpointConfiguration(this EndpointConfiguration endpointConfiguration,
            string endpointName,
            Action<TransportExtensions<RabbitMQTransport>> messageEndpointMappings = null)
        {
            var logFilePath = "D:\\home\\LogFiles\\serilog\\";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext:l}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(logFilePath + endpointName + "-log-{Date}.txt",
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext:l}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            LogManager.Use<SerilogFactory>();

            LogManager.Use<DefaultFactory>().Level(LogLevel.Debug);

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

            var persistance = endpointConfiguration.UsePersistence<SqlPersistence>();
            var connection = ConfigurationManager.AppSettings.Get("PrimaryStorageConnectionString");
            persistance.SqlDialect<SqlDialect.MsSqlServer>();
            persistance.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });
        }
    }
}