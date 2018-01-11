namespace WebServiceIntegration.Fedex
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

        public static string EndpointName => "WebServiceIntegration.Fedex";

        public Host(string connectionString) => this.connectionString = connectionString;

        public async Task<IEndpointInstance> Start()
        {
            try
            {
                var endpointConfiguration = new EndpointConfiguration(EndpointName)
                    .Configure(
                        connectionString, 
                        null
                //routing =>
                //{
                //    routing.RouteToEndpoint(messageType: typeof(Messages.Commands.CreateOrderShipping), destination: "WebServiceIntegration.Shipping");
                //    // routing.RegisterPublisher(typeof(SomeEventType), "myendpoint");
                //});
            );

            endpoint = await Endpoint.Start(endpointConfiguration);
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
                await endpoint?.Stop();
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
                await context.Stop();
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
