namespace MyUnitOfWork
{
    using System;
    using NServiceBus;
    using NServiceBus.Persistence.Raven;
    using NServiceBus.UnitOfWork;
    using Raven.Abstractions.Exceptions;
    using Raven.Client;

    public class MyRavenUnitOfWork : IManageUnitsOfWork
    {
        public IBus Bus { get; set; }
        
        [ThreadStatic]
        static IDocumentSession _session;

        public IDocumentStore Store { get; private set; }

        public IDocumentSession Session
        {
            get { return _session ?? (_session = OpenSession()); }
        }

        public MyRavenUnitOfWork(StoreAccessor storeAccessor)
        {
            Store = storeAccessor.Store;
        }

        public void Begin()
        {
        }

        public void End(Exception ex)
        {
            try
            {
                if (ex == null)
                {
                    SaveChanges();
                }
            }
            finally
            {
                ReleaseSession();
            }
        }

        public void ReleaseSession()
        {
            if (_session == null)
            {
                return;
            }

            _session.Dispose();
            _session = null;
        }

        IDocumentSession OpenSession()
        {
            IMessageContext context = null;

            if (Bus != null)
                context = Bus.CurrentMessageContext;

            var databaseName = GetDatabaseName(context);

            IDocumentSession documentSession;

            if (string.IsNullOrEmpty(databaseName))
                documentSession = Store.OpenSession();
            else
                documentSession = Store.OpenSession(databaseName);

            documentSession.Advanced.AllowNonAuthoritativeInformation = false;
            documentSession.Advanced.UseOptimisticConcurrency = true;

            return documentSession;
        }

        public void SaveChanges()
        {
            if (_session == null)
                return;
            try
            {
                _session.SaveChanges();
            }
            catch (ConcurrencyException ex)
            {
                throw new ConcurrencyException("A saga with the same Unique property already existed in the storage. See the inner exception for further details", ex);
            }
        }

        public static Func<IMessageContext, string> GetDatabaseName = context => String.Empty;
    }
}