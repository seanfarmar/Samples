namespace IPNCallback
{
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;
	using log4net.Appender;
	using log4net.Core;
	using NServiceBus;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class WebApiApplication : System.Web.HttpApplication
	{
		private static IBus _bus;

		protected void Application_Start()
		{
			_bus = Configure.With()
					 .DefaultBuilder()
					 .Log4Net(new DebugAppender { Threshold = Level.Warn })
					 .UseTransport<Msmq>()
					 .PurgeOnStartup(true)
					 .UnicastBus()
					 .RunHandlersUnderIncomingPrincipal(false)
					 .RijndaelEncryptionService()
					 .CreateBus()
					 .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>()
										   .Install());

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		public static IBus Bus 
		{
			get { return _bus; }
		}
	}
}