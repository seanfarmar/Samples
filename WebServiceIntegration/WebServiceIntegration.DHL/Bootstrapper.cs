namespace WebServiceIntegration.DHL
{
    using System;
    using System.Threading.Tasks;
    using Nancy.Hosting.Self;
    using NServiceBus;

    public class Bootstrapper : IWantToRunWhenEndpointStartsAndStops
    {
        private static readonly NancyHost Host = new NancyHost(new Uri("http://localhost:12345"));

        public Task Start(IMessageSession session)
        {
            Host.Start(); // start hosting

            return Task.FromResult(0);
        }

        public Task Stop(IMessageSession session)
        {
            Host.Stop(); // stop hosting

            return Task.FromResult(0);
        }
    }
}