namespace HalloWorld.Client
{
    using System;
    using Commands;
    using NServiceBus;

    public class Application : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'S' to send a message that will say Hallo");
            Console.WriteLine("Press 'E' to send a message that will throw an exception.");
            Console.WriteLine("Press 'Q' to exit.");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                switch (cmd)
                {
                    case "s":

                        Bus.Send<SaySomething>(m => m.What = "Hallo World!");

                        break;
                    case "e":

                        Bus.Send<ThrowException>(m => m.Why = "Don't want to!");

                        break;
                }
            }
        }

        public void Stop()
        {
        }
    }
}
