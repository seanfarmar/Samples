namespace RabbitCompeatingConsumers.Messages.Events
{
    using System;

    public interface IMessage
    {
        Guid EventId { get; set; }
        TimeSpan Duration { get; set; }
        String SentTime { get; set; }
    }
}