namespace MyPublisher
{
	using System;
	using MyMessages;
	using NServiceBus;
	using NServiceBus.Unicast;

	public class ServerEndpoint : IWantToRunWhenTheBusStarts
	{
		public IBus Bus { get; set; }

		public void Run()
		{
			Console.WriteLine("This will publish IEvent, EventMessage, and AnotherEventMessage alternately.");
			Console.WriteLine("Press 'Enter' to publish a message.To exit, Ctrl + C");

			var nextEventToPublish = 0;
			while (Console.ReadLine() != null)
			{
				IMyEvent eventMessage;

				switch (nextEventToPublish)
				{
					case 0:
						eventMessage = Bus.CreateInstance<IMyEvent>();
						nextEventToPublish = 1;
						break;
					case 1:
						eventMessage = new EventMessage();
						nextEventToPublish = 2;
						break;
					default:
						eventMessage = new EventMessage();
						nextEventToPublish = 0;
						break;
				}

				eventMessage.EventId = Guid.NewGuid();
				eventMessage.Time = DateTime.Now.Second > 30 ? (DateTime?) DateTime.Now : null;
				eventMessage.Duration = TimeSpan.FromSeconds(99999D);

				Bus.Publish(eventMessage);

				Console.WriteLine("Published event message type: {1} with Id {0}.", eventMessage.EventId, eventMessage.GetType().ToString());
				Console.WriteLine("==========================================================================");
			}
		}
	}
}