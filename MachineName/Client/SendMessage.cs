namespace Client
{
    using System;
    using Messages;
    using NServiceBus;

    public class SendMessage : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                var id = Guid.NewGuid();

                var myMessage = Bus.CreateInstance<PriceUpdated>(m => { m.PriceId = id; });

                Bus.Send(myMessage);

                Console.WriteLine("==========================================================================");
                Console.WriteLine("Send a new PriceUpdated message with id: {0}", id.ToString("N"));
            }
        }

        public void Stop()
        {
        }
    }
}
