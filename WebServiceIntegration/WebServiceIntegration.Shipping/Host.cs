namespace WebServiceIntegration.Shipping
{
    using System;
    using System.Threading.Tasks;
    using ITOps;
    using NServiceBus;
    using NServiceBus.Logging;

    class Host
    {
        static readonly ILog Log = LogManager.GetLogger<Host>();
        readonly string connectionString;
        IEndpointInstance endpoint;

        public static string EndpointName => "WebServiceIntegration.Shipping";

        public Host(string connectionString) => this.connectionString = connectionString;

        public async Task<IEndpointInstance> Start()
        {
            try
            {
                var endpointConfiguration = new EndpointConfiguration(EndpointName)
                    .Configure(
                        connectionString,
                        routing =>
                        {
                            routing.RouteToEndpoint(assembly: typeof(Messages.Commands.CreateOrderShipping).Assembly, destination: "WebServiceIntegration.Shipping");
                            routing.RouteToEndpoint(assembly: typeof(Messages.Response.DispatchOrderToDhlFailure).Assembly, destination: "WebServiceIntegration.Shipping");
                        });

                endpoint = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                FailFast("Failed to start.", ex);
            }

            return endpoint;
        }

        public async Task Stop()
        {
            try
            {
                if (endpoint != null) await endpoint?.Stop();
            }
            catch (Exception ex)
            {
                FailFast("Failed to stop correctly.", ex);
            }
        }

        async Task OnCriticalError(ICriticalErrorContext context)
        {
            try
            {
                await context.Stop()
                    .ConfigureAwait(false);
            }
            finally
            {
                FailFast($"Critical error, shutting down: {context.Error}", context.Exception);
            }
        }

        void FailFast(string message, Exception exception)
        {
            try
            {
                Log.Fatal(message, exception);
            }
            finally
            {
                Environment.FailFast(message, exception);
            }
        }
    }
}