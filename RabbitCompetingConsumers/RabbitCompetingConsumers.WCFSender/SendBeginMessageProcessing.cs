namespace RabbitCompetingConsumers.WCFSender
{
    using System;
    using Messages.Commands;
    using NServiceBus;

    public class SendBeginMessageProcessing : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send a IBeginMessageProcessing message.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                var id = Guid.NewGuid();

                Bus.Send("RabbitCompetingConsumers.WCFReciver", new BeginMessageProcessing() { Message = "Some text...", MessageKey = new long(), MetaDataKey = new long(), EventId = id});

                Console.WriteLine("==========================================================================");
                Console.WriteLine("Send a new BeginMessageProcessing message with id: {0}", id.ToString("N"));
            }
        }

        public void Stop()
        {
        }
    }
}
