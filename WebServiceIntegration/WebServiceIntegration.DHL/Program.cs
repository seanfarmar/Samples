namespace WebServiceIntegration.DHL
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading.Tasks;
    using Messages.Commands;

    static class Program
    {
        private static CreateOrderShipping _orderShipping;

        public static async Task Main(string[] args)
        {
            var host = new Host(ConfigurationManager.ConnectionStrings["WebServiceIntegration"].ToString());

            // pass this command line option to run as a windows service
            if (args.Contains("--run-as-service"))
            {
                using (var windowsService = new WindowsService(host))
                {
                    ServiceBase.Run(windowsService);
                    return;
                }
            }

            Console.Title = Host.EndpointName;

            var tcs = new TaskCompletionSource<object>();
            Console.CancelKeyPress += (sender, e) => { tcs.SetResult(null); };

           await host.Start().ConfigureAwait(false);

            await Console.Out.WriteLineAsync("Press Ctrl+C to exit...");

            await tcs.Task.ConfigureAwait(false);
            await host.Stop().ConfigureAwait(false);
        }
    }
}