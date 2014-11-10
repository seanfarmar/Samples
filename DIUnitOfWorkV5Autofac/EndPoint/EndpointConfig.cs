namespace Endpoint
{
    using MyUnitOfWork;
    using NServiceBus;
    using NServiceBus.ObjectBuilder;
    using NServiceBus.Persistence;
    using NServiceBus.UnitOfWork;
    using Raven.Client;
    using Raven.Client.Document;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<RavenDBPersistence>();
        }
    }

    public class InjectionConfiguration : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(Registration);
        }

        private void Registration(IConfigureComponents configureComponents)
        {
            var store = new DocumentStore { Url = "http://localhost:8082", DefaultDatabase = "MyDatabase" };

            store.Initialize();

            configureComponents.ConfigureComponent<DocumentStore>(() => store, DependencyLifecycle.SingleInstance);

            configureComponents.ConfigureComponent<IManageUnitsOfWork>(() => new MyRavenUnitOfWork(),
                DependencyLifecycle.InstancePerUnitOfWork);

            configureComponents.ConfigureComponent<IDocumentSession>(
                builder => builder.Build<DocumentStore>().OpenSession(), DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}
