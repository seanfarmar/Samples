namespace MyUnitOfWork
{
    using System;
    using NServiceBus.UnitOfWork;
    using Raven.Client;

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
                Session.SaveChanges();
            }
        }
    }
}