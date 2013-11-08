namespace RabbitCompeatingConsumers.Messages.Events
{
    using System;

    public class BeginFinancialStatementProcessing : IBeginFinancialStatementProcessing
    {
        public Guid EventId { get; set; }
        public TimeSpan Duration { get; set; }
        public string SentTime { get; set; }
        public long MetaDataKey { get; set; }
        public long MessageKey { get; set; }
    }
}