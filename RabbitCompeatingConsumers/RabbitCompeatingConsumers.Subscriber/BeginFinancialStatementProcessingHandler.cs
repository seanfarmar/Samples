namespace RabbitCompeatingConsumers.Subscriber
{
    using System;
    using Messages.Events;
    using NServiceBus;

    public class BeginFinancialStatementProcessingHandler : IHandleMessages<IBeginFinancialStatementProcessing>
    {
        public IBus Bus { get; set; }

        public void Handle(IBeginFinancialStatementProcessing message)
        {
            Console.WriteLine("Handeling Message EventId: {0}", message.EventId);
        }
    }
}
