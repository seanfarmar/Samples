namespace MyUnitOfWork
{
    using System;
    using NServiceBus.UnitOfWork;
    using Raven.Client;
    using Raven.Client.Document;

    public class MyRavenUnitOfWork : IManageUnitsOfWork
    {
        public IDocumentSession Session { get; set; }

        public void Begin()
        {
        }

        public void End(Exception ex)
        {
            if (ex == null)
            {
                Console.WriteLine("In MyRavenUnitOfWork.End, SessionId: {0} ", ((DocumentSession)Session).Id);
                Session.SaveChanges();
            }
        }
    }
}