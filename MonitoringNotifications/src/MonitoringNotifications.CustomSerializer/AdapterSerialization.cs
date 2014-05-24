namespace MonitoringNotifications.CustomSerializer
{
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Features.Categories;
    using NServiceBus.MessageInterfaces.MessageMapper.Reflection;

    public class AdapterSerialization : Feature<Serializers>
    {
        public override void Initialize()
        {
            Configure.Component<MessageMapper>(DependencyLifecycle.SingleInstance);
            Configure.Component<AdapterMessageSerializer>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(t => t.ContentType, ContentTypes.Xml);
        }
    }
}