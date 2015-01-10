
namespace ExternalContainer.Endpoint
{
    using System;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using Messages;
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var builder = new WindsorContainer();

            var container = builder.Install(Configuration.FromAppConfig());

            Configure.With()
                .CastleWindsorBuilder(container)
                .UseInMemoryTimeoutPersister();
        }
    }

    internal class Bootstrap : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 's' to send a message");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
                {
                    case "s":
                        var myMessage = new CalculateTest();

                        Bus.SendLocal(myMessage);

                        Console.WriteLine("==========================================================================");

                        break;
                }
            }
        }

        public void Stop()
        {
        }
    }
}
