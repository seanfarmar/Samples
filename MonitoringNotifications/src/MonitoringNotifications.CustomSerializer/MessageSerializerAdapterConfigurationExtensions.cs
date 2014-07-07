namespace MonitoringNotifications.CustomSerializer
{
    using NServiceBus.Features;
    using NServiceBus.Settings;

    public static class MessageSerializerAdapterConfigurationExtensions
    {
        public static SerializationSettings Adapter(this SerializationSettings settings)
        {
            Feature.Enable<SerializationAdapter>();
            return settings;
        }
    }
}