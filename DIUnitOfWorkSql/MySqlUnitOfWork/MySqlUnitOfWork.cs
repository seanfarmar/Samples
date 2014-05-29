namespace MyUnitOfWork
{
    using System;
    using NHibernate;
    using NServiceBus.UnitOfWork;
    public class MySqlUnitOfWork : IManageUnitsOfWork
    {
        public ISession Session { get; set; }

        public void Begin()
        {
        }

        public void End(Exception ex)
        {
            if (ex == null)
            {
                Console.WriteLine("In MySqlUnitOfWork.End, SessionId: {0} ", ((ISession)Session).Id);
                Session.Flush();
            }
        }
    }
}