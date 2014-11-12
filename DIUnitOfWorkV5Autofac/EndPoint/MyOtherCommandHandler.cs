namespace Endpoint
{
	using System;
	using Messages.Commands;
	using NServiceBus.Saga;
	using Raven.Client;
	using Raven.Client.Document;

    public class MyOtherCommandHandler : Saga<MySagaData>,IAmStartedByMessages<IMyOtherCommand>
    {
        public IDocumentSession Session { get; set; }

		public void Handle(IMyOtherCommand message)
		{
            Console.WriteLine("Handeling a MyOtherCommand of message type: {1} with Id {0}."
					, message.IdGuid, message.GetType());

            // do some work with Raven using MyRavenUnitOfWork
            Console.WriteLine("Session Id:({0}) - Saving MyOtherCommand to raven", ((DocumentSession)Session).Id);
			Console.WriteLine("==========================================================================");

		    Data.IdGuid = message.IdGuid;
            Data.SomeString = message.Name;
            Data.Throw = message.Throw;

            Session.Store(new OtherCommandData
            {
                IdGuid = message.IdGuid,
                Name = message.Name
            });

            if (message.Throw) throw new Exception("Ho nos, MyOtherCommand, we have an issue...");
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<MyOtherCommand>(s => s.IdGuid).ToSaga(m => m.IdGuid);
        }
    }

    public class MySagaData : ContainSagaData
    {
        [Unique]
        public Guid IdGuid { get; set; }

        public bool Throw { get; set; }

        public string SomeString { get; set; }
    }
}
