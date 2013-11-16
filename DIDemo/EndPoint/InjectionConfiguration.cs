namespace Endpoint
{
	using System.Configuration;
	using DataStore;
	using NServiceBus;

	public class InjectionConfiguration : INeedInitialization
    {
		public void Init()
		{
			var connStr = new AppSettingsReader().GetValue("xmlconnection", typeof(string)).ToString();

			Configure.Component<DataStore>(DependencyLifecycle.SingleInstance)

			.ConfigureProperty(ds => ds.ConnectionString, connStr);
		}
	}
}
