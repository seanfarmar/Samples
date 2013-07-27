using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace SimpleMatrix.AccountManager
{
    public class MessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
            .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("SimpleMatrix.InternalMessages"))
            .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("SimpleMatrix.Contracts"));
        }
    }
}

