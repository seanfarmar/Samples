using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace MyPublisher
{
    public class InititalizeSubscriptionStorage : IWantCustomInitialization
    {
        public void Init()
        {
            NServiceBus.Configure.Instance
               .UseNHibernateSubscriptionPersister();
        }
    }
}
