using System;
using NServiceBus;
using HalloWorld.Commands;

namespace HalloWorld.MyEndpoint
{
    public class SendsAtStartup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send another message.To exit, Ctrl + C");         

            while (Console.ReadLine() != null)
            {
                Bus.SendLocal<SaySomething>(m => m.What = "Hallo World! Time: " + DateTime.UtcNow.TimeOfDay.ToString());
            }
        }

        public void Stop(){}
    }
}
