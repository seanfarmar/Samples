namespace WebServiceIntegration.Client
{
    using System;
    using Messages.Commands;
    using NServiceBus;

    public class Bootstapper : IWantToRunWhenBusStartsAndStops
    {
        private CreateOrderShipping _orderShipping;
        public IBus Bus { get; set; }

        public void Start()
        {
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

                            Bus.Send(_orderShipping);

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

                        Bus.Send(exceptionCommand);

                        Console.WriteLine("Sending a exceptionCommand the will throw, message type: {1} with Id {0}."
                            , exceptionCommand.OrderId, exceptionCommand.GetType());
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