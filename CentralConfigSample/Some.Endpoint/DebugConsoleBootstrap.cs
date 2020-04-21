namespace SomeEndpoint
{
    using NServiceBus;
    using Particular.Shared;
    using System;
    using System.Threading.Tasks;

    internal class DebugConsoleBootstrap
    {
        public static async Task Run(string endpointName)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.ApplyEndpointConfiguration(endpointName, EndpointMappings.MessageEndpointMappings());

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            try
            {
                Console.WriteLine("\r\n Bus created and configured; press any key to stop program\r\n");

                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }
    }
}