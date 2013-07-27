using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace SimpleMatrix.WebSite.Infrastructure
{
    public static class WebGlobalInitialization
    {
        public static IBus InitializeNServiceBus()
        {
			Configure.Transactions.Disable();

            return NServiceBus.Configure.With()
                .DefaultBuilder()
                //.ForMvc()
                .XmlSerializer()
                .PurgeOnStartup(false)
                .UnicastBus()
                .RunHandlersUnderIncomingPrincipal(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
        }
    }
}
