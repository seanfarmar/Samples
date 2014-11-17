namespace MyMessages
{
    using System;

    [Serializable]
    public class EventMessage : IMyEvent
    {
        public Guid EventId { get; set; }
        public DateTime? Time { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public interface IMyEvent
    {
        Guid EventId { get; set; }
        DateTime? Time { get; set; }
        TimeSpan Duration { get; set; }
    }
}

namespace MyMessages.Other
{
    using System;

    [Serializable]
    public class AnotherEventMessage : IMyEvent
    {
        public Guid EventId { get; set; }
        public DateTime? Time { get; set; }
        public TimeSpan Duration { get; set; }
    }
}