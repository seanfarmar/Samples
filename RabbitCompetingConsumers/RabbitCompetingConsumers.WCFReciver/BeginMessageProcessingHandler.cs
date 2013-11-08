namespace RabbitCompetingConsumers.WCFReciver
{
    using System;
    using Messages.Commands;
    using Messages.Events;
    using NServiceBus;

    public class BeginMessageProcessingHandler : IHandleMessages<IBeginMessageProcessing>
    {
        public IBus Bus { get; set; }

        public void Handle(IBeginMessageProcessing message)
        {
            Console.WriteLine("Handeling IBeginMessageProcessing");

            Console.WriteLine("Publishing an IBeginFinancialStatementProcessing.");

            var beginFinancialStatementProcessingMessage = new BeginFinancialStatementProcessing
            {
                Duration = new TimeSpan(1, 0, 0, 0),
                EventId = message.EventId,
                MessageKey = 000001,
                MetaDataKey = 0000001,
                SentTime = "sent time"
            };

            Bus.Publish<IBeginFinancialStatementProcessing>(beginFinancialStatementProcessingMessage);
        }
    }
}
