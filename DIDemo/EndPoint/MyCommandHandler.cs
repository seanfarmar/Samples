namespace Endpoint
{
	using System;
	using Interfaces;
	using Messages;
	using Messages.Commands;
	using NServiceBus;

	public class MyCommandHandler : IHandleMessages<IMyCommand>
	{
		public IDataStore DataStore { get; set; }

		public void Handle(IMyCommand message)
		{
			DataStore.NumberOfUsagesOfThisInstance ++;

			Console.WriteLine("Handeling a command of message type: {1} with Id {0}."
					, message.IdGuid, message.GetType());

			Console.WriteLine("My connection string is: {0}, numberOfUages: {1}", DataStore.ConnectionString, DataStore.NumberOfUsagesOfThisInstance);
			Console.WriteLine("==========================================================================");
		}
	}
}
