namespace WebServiceIntegration.DHL
{
    using System;
    using Nancy.Hosting.Self;
    using NServiceBus;

    public class Bootstrapper : IWantToRunWhenBusStartsAndStops
    {
        private static readonly NancyHost Host = new NancyHost(new Uri("http://localhost:12345"));

        public void Start()
        {
            Host.Start(); // start hosting
        }

        public void Stop()
        {
            Host.Stop(); // stop hosting
        }
    }
}