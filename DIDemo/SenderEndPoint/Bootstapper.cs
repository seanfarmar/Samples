namespace SenderEndPoint
{
	using System;
	using Messages.Commands;
	using NServiceBus;

	public class Bootstapper : IWantToRunWhenBusStartsAndStops
    {
		public IBus Bus { get; set; }

		public void Start()
		{
			Console.WriteLine("This will send a command Message.");
			Console.WriteLine("Press 'Enter' to send a message. To exit, Ctrl + C");

			while (Console.ReadLine() != null)
			{
				IMyCommand myCommand = Bus.CreateInstance<IMyCommand>(m =>
				{
					m.IdGuid = Guid.NewGuid();
					m.Name = "My Name is Demo!";
				});

				Bus.Send(myCommand);

				Console.WriteLine("Send a command message type: {1} with Id {0}."
					, myCommand.IdGuid, myCommand.GetType());
				Console.WriteLine("==========================================================================");
			}
		}

		public void Stop()
		{
		}
    }
}
