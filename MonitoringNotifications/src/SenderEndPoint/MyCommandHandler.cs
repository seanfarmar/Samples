namespace MonitoringNotifications.SenderEndPoint
{
    using System;
    using Messages.Commands;
    using NServiceBus;

    public class MyCommandHandler : IHandleMessages<MyCommand>
	{
        public void Handle(MyCommand message)
		{
            Console.WriteLine("Handeling a MyCommand of message type: {1} with Id {0}.", message.IdGuid, message.GetType());

			Console.WriteLine("==========================================================================");

            if (message.Throw) throw new Exception("Ho nos, MyCommand, we have an issue...");
        }
	}
}
