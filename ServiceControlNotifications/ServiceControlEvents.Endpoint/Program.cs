namespace ServiceControlEvents.Endpoint
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure;
    using Microsoft.Azure.WebJobs;
    using NServiceBus;

    public class Program
    {
        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
#if DEBUG
            var endpointName = CloudConfigurationManager.GetSetting("EndpointName");

            Console.Title = typeof(Program).Assembly.GetName().Name;

            await DebugConsoleBootstrap.Run(endpointName);
#else
            var config = new JobHostConfiguration();

            var host = new JobHost(config);

            Console.WriteLine("Starting host");
            host.Call(typeof(Program).GetMethod(nameof(Program.AsyncMain)));
            host.RunAndBlock();
#endif
        }

        [NoAutomaticTrigger]
        public static async Task AsyncMain(CancellationToken cancellationToken)
        {
            var endpointName = CloudConfigurationManager.GetSetting("EndpointName");

            Console.Title = endpointName;
            var endpointConfiguration = new EndpointConfiguration(endpointName);

            endpointConfiguration.ApplyEndpointConfiguration(EndpointMappings.MessageEndpointMappings());

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(3000, cancellationToken)
                    .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}