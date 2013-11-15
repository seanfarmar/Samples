namespace MyMessages
{
	using System;
	using NServiceBus;
	/// <summary>
	/// comments
	/// </summary>

	public interface IMyEvent : IMessage
	{
		Guid EventId { get; set; }
		DateTime? Time { get; set; }
		TimeSpan Duration { get; set; }
	}

	[Serializable]
    public class EventMessage : IMyEvent
	{
        public Guid EventId { get; set; }

		public DateTime? Time { get; set; }
		public TimeSpan Duration { get; set; }
    }
}