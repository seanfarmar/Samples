namespace MonitoringNotifications.CustomSerializer
{
    using NServiceBus.Features;
    using NServiceBus.Settings;

    public static class AdapterMessageSerializerConfigurationExtensions
    {
        public static SerializationSettings Adapter(this SerializationSettings settings)
        {
            Feature.Enable<AdapterSerialization>();
            return settings;
        }
    }
}