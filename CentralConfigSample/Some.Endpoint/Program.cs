using NServiceBus;
using Particular.Shared;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace SomeEndpoint
{
    public class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var endpointName = ConfigurationManager.AppSettings.Get("EndpointName");

            Console.Title = typeof(Program).Assembly.GetName().Name;

            await DebugConsoleBootstrap.Run(endpointName);
        }

        public static async Task AsyncMain(CancellationToken cancellationToken)
        {
            var endpointName = ConfigurationManager.AppSettings.Get("EndpointName");

            Console.Title = endpointName;
            var endpointConfiguration = new EndpointConfiguration(endpointName);

            endpointConfiguration.ApplyEndpointConfiguration(endpointName, EndpointMappings.MessageEndpointMappings());

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(3000, cancellationToken)
                    .ConfigureAwait(false);
            }

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}