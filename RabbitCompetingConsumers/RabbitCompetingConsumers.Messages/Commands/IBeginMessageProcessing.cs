namespace RabbitCompetingConsumers.Messages.Commands
{
    using System;

    public interface IBeginMessageProcessing
    {
        long MetaDataKey { get; set; }
        long MessageKey { get; set; }
        string Message { get; set; }
        Guid EventId { get; set; }
    }
}