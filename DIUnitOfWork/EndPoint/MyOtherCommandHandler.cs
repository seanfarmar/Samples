namespace Endpoint
{
	using System;
	using Messages.Commands;
	using NServiceBus;
	using Raven.Client;
	using Raven.Client.Document;

    public class MyOtherCommandHandler : IHandleMessages<IMyOtherCommand>
	{
        public IDocumentSession Session { get; set; }

		public void Handle(IMyOtherCommand message)
		{
            Console.WriteLine("Handeling a MyOtherCommand of message type: {1} with Id {0}."
					, message.IdGuid, message.GetType());

            // do some work with Raven using MyRavenUnitOfWork
            Console.WriteLine("Session Id:({0}) - Saving MyOtherCommand to raven", ((DocumentSession)Session).Id);
			Console.WriteLine("==========================================================================");

            Session.Store(new OtherCommandData
            {
                IdGuid = message.IdGuid,
                Name = message.Name
            });

            if (message.Throw) throw new Exception("Ho nos, MyOtherCommand, we have an issue...");
        }
	}
}
