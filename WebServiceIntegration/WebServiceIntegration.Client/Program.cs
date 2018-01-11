namespace WebServiceIntegration.Client
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading.Tasks;
    using Messages.Commands;
    using NServiceBus;

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

            IEndpointInstance endpomEndpointInstance = await host.Start();

            //await Console.Out.WriteLineAsync("Press Ctrl+C to exit...");

            Console.WriteLine("Press 's' to send lots of commands");
            Console.WriteLine("Press 'e' to send a command that will throw an exception.");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
                {
                    case "s":
                        for (int i = 0; i < 30; i++)
                        {
                            _orderShipping = new CreateOrderShipping
                            {
                                OrderId = Guid.NewGuid(),
                                OrderCountryCode = "IRL",
                                OrderNumber = i
                            };

                            await endpomEndpointInstance.Send(_orderShipping).ConfigureAwait(false);

                            Console.WriteLine("Send a MyOtherCommand message number {2} type: {1} with Id {0}."
                                , _orderShipping.OrderId
                                , _orderShipping.GetType(), i);
                            Console.WriteLine(
                                "==========================================================================");
                        }
                        break;

                    case "e":
                        var exceptionCommand = new CreateOrderShipping
                        {
                            OrderId = Guid.NewGuid(),
                            OrderCountryCode = "IRL",
                            OrderNumber = 100,
                            ThrowException = true
                        };

                        await endpomEndpointInstance.Send(exceptionCommand).ConfigureAwait(false);

                        Console.WriteLine("Sending a exceptionCommand the will throw, message type: {1} with Id {0}."
                            , exceptionCommand.OrderId, exceptionCommand.GetType());
                        Console.WriteLine("==========================================================================");

                        break;
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press 's' to send lots of commands");
                Console.WriteLine("Press 'e' to send a command that will throw an exception.");
            }

            await tcs.Task;
            await host.Stop();
        }
    }
}
