namespace BackEnd
{
	using System;
	using Messages.Commands;
	using NServiceBus;

	public class SayHalloHandler : IHandleMessages<SayHallo>
    {
		public void Handle(SayHallo message)
		{
			Console.WriteLine("Handeling Message SayHallo: What: {0} guid: {1}, ", message.What, message.Guid);
		}
    }
}
