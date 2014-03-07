namespace Server
{
    using System;
    using Messages;
    using NServiceBus;

    class PriceUpdatedHandler : IHandleMessages<PriceUpdated>
    {
        public void Handle(PriceUpdated message)
        {
            Console.WriteLine("Handeling PriceUpdated messahe with id: {0}", message.PriceId);
        }
    }
}
