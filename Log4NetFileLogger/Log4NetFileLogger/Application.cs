using System;
using NServiceBus;

namespace Log4NetFileLogger
{
    public class Application : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'S' to send a message or 'Q' to exit.");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                switch (cmd)
                {
                    case "s":
                        var m = new MyMessage { Id = Guid.NewGuid() };
                        Bus.Send(m);

                        Console.WriteLine("Sent a message with Id: {0}", m.Id);

                        Console.WriteLine("Press 'S' to send a message or 'Q' to exit.");

                        Console.WriteLine("============================================");

                        break;
                }
            }
        }

        public void Stop(){}
    }
}