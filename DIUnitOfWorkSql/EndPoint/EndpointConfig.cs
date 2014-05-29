namespace Endpoint
{
    using MyUnitOfWork;
    using NServiceBus;
    using NServiceBus.UnitOfWork;
    using Raven.Client;
    using Raven.Client.Document;
    using StructureMap;
    using StructureMap.Graph;

    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization, AsA_Server
    {
	    public void Init()
	    {
            Configure.With()
                .StructureMapBuilder()
                .RavenPersistence();
	    }
    }

    public class InjectionConfiguration : IWantCustomInitialization
    {
        public void Init()
        {
            var store = new DocumentStore { Url = "http://localhost:8082", DefaultDatabase = "MyDatabase" };

            store.Initialize();

            ObjectFactory.Configure(c =>
            {
                c.For<IDocumentStore>()
                    .Singleton()
                    .Use(store);

                c.For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                        .OpenSession());

                PluginCache.AddFilledType(typeof(IDocumentSession));

                c.For<IManageUnitsOfWork>()
                    .Use<MyRavenUnitOfWork>();
            });
        }
    }
}
