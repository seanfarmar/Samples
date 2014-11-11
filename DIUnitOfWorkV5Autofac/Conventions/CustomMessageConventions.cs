namespace Conventions
{
    using NServiceBus;

    public static class CustomMessageConventions
    {
        public static void ApplyCustomMessageConventions(this BusConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Commands"));
        }
    }
}
