namespace Endpoint
{
	using System;
	using Messages.Commands;
	using MyUnitOfWork;
	using NServiceBus;
	using Raven.Client;
	using Raven.Client.Document;

    public class MyCommandHandler : IHandleMessages<IMyCommand>
	{
        public MyRavenUnitOfWork MyRavenUnitOfWork { get; set; }
        
        readonly IDocumentSession _session;

        public MyCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

		public void Handle(IMyCommand message)
		{
            Console.WriteLine("Handeling a command of message type: {1} with Id {0}."
					, message.IdGuid, message.GetType());

            // do some work with Raven using MyRavenUnitOfWork
            Console.WriteLine("Session Id:({0}) - Saving command to raven", ((DocumentSession)_session).Id);
			Console.WriteLine("==========================================================================");

            _session.Store(new CommandData
            {
                IdGuid = message.IdGuid,
                Name = message.Name
            });

            if (message.Throw) throw new Exception("Ho nos, we have an issue...");
        }
	}
}
