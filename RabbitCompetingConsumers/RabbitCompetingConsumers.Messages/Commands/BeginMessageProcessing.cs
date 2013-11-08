namespace RabbitCompetingConsumers.Messages.Commands
{
    using System;

    public class BeginMessageProcessing : IBeginMessageProcessing
    {
        public long MetaDataKey { get; set; }

        public long MessageKey { get; set; }

        public string Message { get; set; }
        
        public Guid EventId { get; set; }
    }
}
